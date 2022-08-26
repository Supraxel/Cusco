namespace Cusco.FixingCSharp;

public static class DictionaryExtensions
{
  public static IReadOnlyDictionary<TKey, TReadOnlyValue> Covarying<TKey, TValue, TReadOnlyValue>(
    this IReadOnlyDictionary<TKey, TValue> self
  ) where TValue : TReadOnlyValue
    => new CovaryingReadOnlyDictionaryWrapper<TKey, TValue, TReadOnlyValue>(self);
}
