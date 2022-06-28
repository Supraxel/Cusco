namespace Cusco.FixingCSharp;

internal sealed class CovaryingReadOnlyDictionaryWrapper<TKey, TValue, TReadOnlyValue> : IReadOnlyDictionary<TKey, TReadOnlyValue> where TValue : TReadOnlyValue
{
    private readonly IReadOnlyDictionary<TKey, TValue> _dictionary;
    public IEnumerable<TKey> Keys => _dictionary.Keys;

    public CovaryingReadOnlyDictionaryWrapper(IReadOnlyDictionary<TKey, TValue> dictionary)
    {
        _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
    }
    
    public bool ContainsKey(TKey key) { return _dictionary.ContainsKey(key); }

    public bool TryGetValue(TKey key, out TReadOnlyValue value)
    {
        var result = _dictionary.TryGetValue(key, out var v);
        value = v;
        return result;
    }

    public IEnumerable<TReadOnlyValue> Values => _dictionary.Values.Cast<TReadOnlyValue>();

    public TReadOnlyValue this[TKey key] => _dictionary[key];

    public int Count => _dictionary.Count;

    public IEnumerator<KeyValuePair<TKey, TReadOnlyValue>> GetEnumerator()
    {
        return _dictionary
            .Select(x => new KeyValuePair<TKey, TReadOnlyValue>(x.Key, x.Value))
            .GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}