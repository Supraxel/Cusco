using Cusco.LowLevel;

namespace Cusco.ReactiveX;

public abstract partial class Observable<T>
{
  public Observable<TCombined> CombineLatest<TOther, TCombined>(Observable<TOther> other, Func<T, TOther, TCombined> combiner)
  {
    if (null == other) throw new ArgumentNullException(nameof(other));
    if (null == combiner) throw new ArgumentNullException(nameof(combiner));

    return Observable.Create<TCombined>(dispatchQueue, observer =>
    {
      var disposeBag = new DisposeBag();
      var selfCompleted = false;
      var selfValue = Option<T>.None();
      var otherCompleted = false;
      var otherValue = Option<TOther>.None();

      void CompleteIfNeeded()
      {
        if (selfCompleted == otherCompleted)
          observer.OnCompleted();
      }

      void EmitIfNeeded()
      {
        if (selfValue.isSome && otherValue.isSome)
          observer.OnNext(combiner(selfValue.Unwrap(), otherValue.Unwrap()));
      }

      Subscribe(
          onNext: value =>
          {
            selfValue = value;
            EmitIfNeeded();
          },
          onError: exc =>
          {
            observer.OnError(exc);
            disposeBag.Dispose();
          },
          onComplete: () =>
          {
            selfCompleted = true;
            CompleteIfNeeded();
          })
        .DisposedBy(disposeBag);

      other.Subscribe(
          onNext: value =>
          {
            otherValue = value;
            EmitIfNeeded();
          },
          onError: exc =>
          {
            observer.OnError(exc);
            disposeBag.Dispose();
          },
          onComplete: () =>
          {
            otherCompleted = true;
            CompleteIfNeeded();
          })
        .DisposedBy(disposeBag);

      return disposeBag;
    });
  }
}
