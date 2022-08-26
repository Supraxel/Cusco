namespace Cusco;

public readonly struct Result<T>
{
  private readonly Exception error;
  private readonly T value;
  private readonly ResultType type;

  public static Result<T> Ok(T value) => new(value, default, ResultType.Ok);
  public static Result<T> Err(Exception error) => new(default, error, ResultType.Err);

  public static implicit operator Result<T>(T value) => Ok(value);
  public static implicit operator Result<T>(Exception error) => Err(error);

  private Result(T value, Exception error, ResultType type)
  {
    this.error = error?.Enhance();
    this.value = value;
    this.type = type;
  }

  public Option<Exception> err => type switch
  {
    ResultType.Err => Option<Exception>.Some(error),
    _ => Option<Exception>.None()
  };

  public bool isErr => type == ResultType.Err;

  public bool isOk => type == ResultType.Ok;

  public Option<T> ok => type switch
  {
    ResultType.Ok => Option<T>.Some(value),
    _ => Option<T>.None(),
  };

  public Result<U> And<U>(Result<U> rhs) => type switch
  {
    ResultType.Ok => rhs,
    _ => Result<U>.Err(error),
  };

  public Result<U> AndThen<U>(Func<T, U> op) => type switch
  {
    ResultType.Ok => Result<U>.Ok(op(value)),
    _ => Result<U>.Err(error),
  };

  public T Expect(FormattableString message) => type switch
  {
    ResultType.Ok => value,
    _ => CuscoRT.Panic<T>($"Result.Expect on a Err value: {message}"),
  };

  public Exception ExpectErr(FormattableString message) => type switch
  {
    ResultType.Err => error,
    _ => CuscoRT.Panic<Exception>($"Result.ExpectErr on an OK value: {message}"),
  };

  public Result<U> Map<U>(Func<T, U> op) => type switch
  {
    ResultType.Ok => Result<U>.Ok(op(value)),
    _ => Result<U>.Err(error),
  };

  public Result<U> MapOr<U>(U defaultValue, Func<T, U> op) => type switch
  {
    ResultType.Ok => op(value),
    _ => defaultValue,
  };

  public Result<U> MapOrElse<U>(Func<Exception, U> defaultOp, Func<T, U> op) => type switch
  {
    ResultType.Ok => op(value),
    _ => defaultOp(error),
  };

  public Result<T> Or(Result<T> rhs) => type switch
  {
    ResultType.Ok => this,
    _ => rhs,
  };

  public override string ToString()
    => isOk ? value?.ToString() : error.ToString();

  public T Unwrap() => type switch
  {
    ResultType.Ok => value,
    _ => CuscoRT.Panic<T>("Result.Unwrap on a Err value"),
  };

  public Exception UnwrapErr() => type switch
  {
    ResultType.Err => error,
    _ => CuscoRT.Panic<Exception>($"Result.UnwrapErr on an OK value"),
  };

  public T UnwrapOr(T defaultValue) => type switch
  {
    ResultType.Ok => value,
    _ => defaultValue,
  };

  public T UnwrapOrDefault() => type switch
  {
    ResultType.Ok => value,
    _ => default,
  };

  public T UnwrapOrElse(Func<T> op) => type switch
  {
    ResultType.Ok => value,
    _ => op(),
  };

  private enum ResultType : byte
  {
    Ok,
    Err,
  }
}
