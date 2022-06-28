﻿using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using JetBrains.Annotations;

namespace Cusco.Dispatch;

[PublicAPI]
public sealed class Future<T>
{
    private readonly SingleList<Action> callbacks;
    private readonly CancellationToken cancellationToken;
    private readonly DispatchQueue dispatchQueue;
    private Box<Result<T>> result;

    internal Future(DispatchQueue dispatchQueue, CancellationToken cancellationToken = default)
    {
        this.callbacks = new SingleList<Action>();
        this.cancellationToken = cancellationToken;
        this.dispatchQueue = dispatchQueue;
        this.result = Box<Result<T>>.nil;
    }

    public bool isDone => result != null;
    public bool isErr => isDone && result!.asRef.isErr;
    public bool isOk => isDone && result!.asRef.isOk;

    /// <summary>
    /// Create a new future, replacing its <see cref="CancellationToken"/>.
    /// </summary>
    /// <param name="token">Token to be used</param>
    /// <returns>A future with the specified <see cref="CancellationToken"/></returns>
    [Pure]
    public Future<T> CancellableBy(CancellationToken token)
    {
        var promise = dispatchQueue.MakePromise<T>(token);
        
        Cascade(promise);
        
        return promise;
    }
    
    public Future<T> Cascade(Promise<T> promise)
    {
        if (promise.isInvalid) throw new NullReferenceException(nameof(promise));
        
        AddCallback(() => promise.Complete(result!.asRef));

        return this;
    }

    public Future<T> CascadeIfOk(Promise<T> promise)
    {
        if (promise.isInvalid) throw new NullReferenceException(nameof(promise));
        
        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;

            if (resultRef.isOk)
                promise.Complete(result!.asRef);
        });

        return this;
    }

    public Future<T> CascadeIfErr(Promise<T> promise)
    {
        if (promise.isInvalid) throw new NullReferenceException(nameof(promise));
        
        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;

            if (resultRef.isErr) return;
                promise.Complete(result!.asRef);
        });

        return this;
    }

    public Future<T> Do(Action<Result<T>> block)
    {
        if (block == null) throw new ArgumentNullException(nameof(block));
        
        AddCallback(() => block.TryInvokeSafely(result!.asRef, out _));

        return this;
    }

    public Future<T> DoIfOk(Action<T> block)
    {
        if (block == null) throw new ArgumentNullException(nameof(block));

        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;

            if (resultRef.isOk)
                block.TryInvokeSafely(result!.asRef.Unwrap(), out _);
        });

        return this;
    }

    public Future<T> DoIfErr(Action<Exception> block)
    {
        if (block == null) throw new ArgumentNullException(nameof(block));
        
        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;

            if (resultRef.isErr)
                block.TryInvokeSafely(result!.asRef.UnwrapErr(), out _);
        });

        return this;
    }

    public Future<T> DoIfErr<TException>(Action<TException> block) where TException: Exception
    {
        if (block == null) throw new ArgumentNullException(nameof(block));

        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;

            if (resultRef.isErr && resultRef.UnwrapErr() is TException castedExc)
                block.TryInvokeSafely(castedExc, out _);
        });

        return this;
    }

    [MustUseReturnValue]
    public Future<T> Hop(DispatchQueue queue)
    {
        var promise = (queue ?? throw new ArgumentNullException(nameof(queue))).MakePromise<T>(cancellationToken);

        Cascade(promise);

        return promise;
    }
    
    [MustUseReturnValue]
    public Future<T> Recover(Func<Exception, T> recovery)
    {
        if (null == recovery)
            throw new ArgumentNullException(nameof(recovery));
        
        var next = dispatchQueue.MakePromise<T>(cancellationToken);

        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;
            switch (resultRef)
            {
                case { isOk: true }:
                {
                    next.Complete(resultRef.Unwrap());
                    break;
                }
                case { isErr: true }:
                {
                    if (false == recovery.TryInvokeSafely(resultRef.UnwrapErr(), out var t, out var exc))
                        next.CompleteWithErr(exc);
                    else
                        next.Complete(t);
                    break;
                }
            }
        });
        
        return next;
    }

    [MustUseReturnValue]
    public Future<T> Recover<TException>(Func<TException, T> recovery) where TException: Exception
    {
        if (null == recovery)
            throw new ArgumentNullException(nameof(recovery));
        return Recover(exc =>
        {
            if (exc is TException castedExc)
                return recovery(castedExc);
            
            throw exc.Rethrow();
        });
    }

    [MustUseReturnValue]
    public Future<T> RecoverAsync(Func<Exception, Future<T>> recovery)
    {
        if (null == recovery)
            throw new ArgumentNullException(nameof(recovery));
        
        var next = dispatchQueue.MakePromise<T>(cancellationToken);

        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;
            switch (resultRef)
            {
                case { isOk: true }:
                {
                    next.Complete(resultRef.Unwrap());
                    break;
                }
                case { isErr: true }:
                {
                    if (false == recovery.TryInvokeSafely(resultRef.UnwrapErr(), out var futureT, out var exc))
                        next.CompleteWithErr(exc);
                    else
                        futureT.Cascade(next);
                    break;
                }
            }
        });
        
        return next;
    }

    [MustUseReturnValue]
    public Future<T> RecoverAsync<TException>(Func<TException, Future<T>> recovery) where TException: Exception
    {
        if (null == recovery)
            throw new ArgumentNullException(nameof(recovery));
        return RecoverAsync(exc =>
        {
            if (exc is TException castedExc)
                return recovery(castedExc);
            
            throw exc.Rethrow();
        });
    }

    [MustUseReturnValue]
    public Future<U> Select<U>(Func<T, U> transform)
    {
        if (null == transform)
            throw new ArgumentNullException(nameof(transform));
        
        var next = dispatchQueue.MakePromise<U>(cancellationToken);

        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;
            switch (resultRef)
            {
                case { isOk: true }:
                {
                    if (false == transform.TryInvokeSafely(resultRef.Unwrap(), out var u, out var exc))
                        next.CompleteWithErr(exc);
                    else
                        next.Complete(u);

                    break;
                }
                case { isErr: true }:
                {
                    next.CompleteWithErr(resultRef.UnwrapErr());
                    break;
                }
            }
        });
        
        return next;
    }

    [MustUseReturnValue]
    public Future<U> SelectAsync<U>(Func<T, Future<U>> transformAsync)
    {
        if (null == transformAsync)
            throw new ArgumentNullException(nameof(transformAsync));
        
        var next = dispatchQueue.MakePromise<U>(cancellationToken);

        AddCallback(() =>
        {
            ref readonly Result<T> resultRef = ref result!.asRef;
            switch (resultRef)
            {
                case { isOk: true }:
                {
                    if (false == transformAsync.TryInvokeSafely(resultRef.Unwrap(), out var futureU, out var exc))
                        next.CompleteWithErr(exc);
                    else
                        futureU.Cascade(next);

                    break;
                }
                case { isErr: true }:
                {
                    next.CompleteWithErr(resultRef.UnwrapErr());
                    break;
                }
            }
        });
        
        return next;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddCallback(Action callback)
        => dispatchQueue.DispatchImmediate(() => AddCallback0(callback));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddCallback0(Action callback)
    {
        if (null != result)
            DispatchQueueImpl.ExecuteWorkloadSafely(callback);
        else
            callbacks.Add(callback);
    }

    private void RunCallbacks0()
    {
        dispatchQueue.AssertOnQueue();

        callbacks.ForEach(DispatchQueueImpl.ExecuteWorkloadSafely);
        callbacks.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetResult(Result<T> value)
    {
        var boxedResult = new Box<Result<T>>(value);
        if (null != Interlocked.CompareExchange(ref this.result, boxedResult, default))
            return;

        dispatchQueue.DispatchImmediate(RunCallbacks0);
    }
}

public static class FutureEmptyExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Future<Empty> DoIfOk(this Future<Empty> self, Action block)
        => self.DoIfOk(_ => block());
    
    [MethodImpl(MethodImplOptions.AggressiveInlining), MustUseReturnValue]
    public static Future<U> Select<U>(this Future<Empty> self, Func<U> block)
        => self.Select(_ => block());
    
    [MethodImpl(MethodImplOptions.AggressiveInlining), MustUseReturnValue]
    public static Future<U> SelectAsync<U>(this Future<Empty> self, Func<Future<U>> block)
        => self.SelectAsync(_ => block());
}