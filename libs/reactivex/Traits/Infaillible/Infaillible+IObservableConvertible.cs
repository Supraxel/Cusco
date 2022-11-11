namespace Cusco.ReactiveX;

public readonly partial record struct Infaillible<T> : IObservableConvertible<T>
{
  public Observable<T> AsObservable() => observable;
}

public static class ObservableConvertibleInfaillibleExtensions
{
  public static Infaillible<T> AsInfaillible<T>(this IObservableConvertible<T> self, T element)
    => new(self.AsObservable().CatchAndReturn(element));

  public static Infaillible<T> AsInfaillible<T>(this IObservableConvertible<T> self, Infaillible<T> infaillible)
    => new(self.AsObservable().Catch(exc => infaillible.AsObservable()));

  public static Infaillible<T> AsInfaillible<T>(this IObservableConvertible<T> self, Func<Exception, Infaillible<T>> recover)
    => new(self.AsObservable().Catch(exc => recover(exc).AsObservable()));
}
