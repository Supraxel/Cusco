using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Cusco.Dispatch;

[PublicAPI]
public struct Promise<T>
{
    public readonly Future<T> future;
    public readonly DispatchQueue queue;

    internal bool isInvalid => future == null || queue == null;
    
    public Promise(DispatchQueue queue, CancellationToken cancellationToken = default)
    {
        var f = future = new(queue, cancellationToken);
        this.queue = queue;
        
        cancellationToken.Register(() =>
        {
            queue.DispatchImmediate(() =>
            {
                f.SetResult(Result<T>.Err(new OperationCanceledException(cancellationToken).Enhance()));
            });
        });
    }

    public static implicit operator Future<T>(Promise<T> promise) => promise.future ?? throw new NullReferenceException();

    public void Complete(Result<T> result)
        => future.SetResult(result);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CompleteWithErr(Exception exception)
        => Complete(Result<T>.Err(exception));
}