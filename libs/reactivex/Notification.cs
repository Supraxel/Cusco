namespace Cusco.ReactiveX;

public readonly struct Notification<T>
{
  private enum Kind
  {
    Complete,
    Next,
    Error
  }

  private readonly Kind _type;
  private readonly Exception _error;
  private readonly T _value;

  public bool isComplete => _type == Kind.Complete;
  public bool isErr => _type == Kind.Error;
  public bool isNext => _type == Kind.Next;

  public Option<Exception> error => _type switch
  {
    Kind.Error => Option<Exception>.Some(_error),
    _ => Option<Exception>.None(),
  };

  public Option<T> next => _type switch
  {
    Kind.Next => Option<T>.Some(_value),
    _ => Option<T>.None(),
  };

  public static Notification<T> Completed() => default;
  public static Notification<T> WithError(Exception error) => new(Kind.Error, default, error);
  public static Notification<T> WithNextValue(T value) => new(Kind.Next, value, null);

  private Notification(Kind type, T value, Exception error)
  {
    _type = type;
    _value = value;
    _error = error;
  }

  public void ExpectComplete(FormattableString message)
  {
    switch (_type)
    {
      case Kind.Complete:
        return;
      default:
        CuscoRT.Panic($"Notification.ExpectComplete on an {_type} value: {message}");
        break;
    }
  }

  public Exception ExpectError(FormattableString message) => _type switch
  {
    Kind.Error => _error,
    _ => CuscoRT.Panic<Exception>($"Notification.ExpectError on an {_type} value: {message}")
  };

  public T ExpectNext(FormattableString message) => _type switch
  {
    Kind.Next => _value,
    _ => CuscoRT.Panic<T>($"Notification.ExpectNext on an {_type} value: {message}")
  };

  public override string ToString() => _type switch
  {
    Kind.Complete => $"{base.ToString()}(complete)",
    Kind.Error => $"{base.ToString()}(error): {_error.GetType().Name}: {_error.Message}",
    Kind.Next => $"{base.ToString()}(next): {_value}",
    _ => CuscoRT.Unreachable<string>(),
  };
}
