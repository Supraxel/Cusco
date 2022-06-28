using System;

namespace Cusco;

public readonly struct Option<T>
{
    private readonly bool hasValue;
    private readonly T value;

    public static Option<T> None()
        => new();
    public static Option<T> Some(T value)
        => new(value);
        
    public static implicit operator Option<T>(T value)
        => Some(value);

    private Option(T value)
    {
        this.hasValue = true;
        this.value = value;
    }
        
    public bool isNone
        => false == hasValue;
        
    public bool isSome
        => hasValue;

    public Option<U> And<U>(Option<U> rhs)
        => hasValue ? rhs : Option<U>.None();

    public Option<U> AndThen<U>(Func<T, U> op)
        => hasValue ? op(value) : Option<U>.None();

    public T Expect(FormattableString message)
        => hasValue ? value : CuscoRT.Panic<T>($"Option.Expect on a None value: {message}");

    public Option<T> Filter(Func<T, bool> predicate)
        => hasValue && predicate(value) ? this : None();
        
    public Option<U> Map<U>(Func<T, U> op)
        => hasValue ? op(value) : Option<U>.None();

    public Result<U> MapOr<U>(U defaultValue, Func<T, U> op)
        => hasValue ? op(value) : defaultValue;

    public Result<U> MapOrElse<U>(Func<U> defaultOp, Func<T, U> op)
        => hasValue ? op(value) : defaultOp();

    public Option<T> Or(Option<T> orValue)
        => hasValue ? value : orValue;

    public Option<T> OrElse(Func<Option<T>> op)
        => hasValue ? value : op();

    public Result<T> OkOr(Exception err)
        => hasValue ? value : err;

    public Result<T> OkOrElse(Func<Exception> err)
        => hasValue ? value : err();

    public T Unwrap()
        => hasValue ? value : CuscoRT.Panic<T>("Option.Unwrap on a Err value");

    public T UnwrapOr(T defaultValue)
        => hasValue ? value : defaultValue;

    public T UnwrapOrDefault()
        => hasValue ? value : default;

    public T UnwrapOrElse(Func<T> op)
        => hasValue ? value : op();

    public Option<T> Xor(Option<T> rhs)
        => (hasValue, rhs.hasValue) switch
        {
            (true, false) => value,
            (false, true) => rhs.value,
            _ => None(),
        };

    public Option<(T, U)> Zip<U>(Option<U> rhs)
        => hasValue && rhs.hasValue ? (value, rhs.value) : Option<(T, U)>.None();

    public Option<R> ZipWith<U, R>(Option<U> rhs, Func<T, U, R> op)
        => hasValue && rhs.hasValue ? op(value, rhs.value) : Option<R>.None();
}