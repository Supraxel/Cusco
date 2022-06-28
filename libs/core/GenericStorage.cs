using System.Collections.Concurrent;

namespace Cusco;

public interface IReadOnlyGenericStorage
{
    bool ContainsKey<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>;
    TValue Get<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>;
    bool TryGet<TKey, TValue>(out TValue value) where TKey : struct, GenericStorageKey<TValue>;
}

public interface IGenericStorage : IReadOnlyGenericStorage
{
    bool Remove<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>;
    void Set<TKey, TValue>(TValue value) where TKey : struct, GenericStorageKey<TValue>;
}

public sealed class GenericStorage : IGenericStorage
{
    private readonly Dictionary<Type, object> storage = new();

    public bool ContainsKey<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        return storage.ContainsKey(key);
    }

    public TValue Get<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        return (TValue)storage[key];
    }

    public bool Remove<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        return storage.Remove(key);
    }

    public void Set<TKey, TValue>(TValue value) where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        storage[key] = value;
    }

    public bool TryGet<TKey, TValue>(out TValue value) where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        if (storage.TryGetValue(key, out var rawValue))
        {
            value = (TValue)rawValue;
            return true;
        }

        value = default;
        return false;
    }
}

public sealed class ConcurrentGenericStorage : IGenericStorage
{
    private readonly ConcurrentDictionary<Type, object> storage = new();
    
    public bool ContainsKey<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        return storage.ContainsKey(key);
    }

    public TValue Get<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        return (TValue)storage[key];
    }

    public bool Remove<TKey, TValue>() where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        return storage.Remove(key, out _);
    }

    public void Set<TKey, TValue>(TValue value) where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        storage[key] = value;
    }

    public bool TryGet<TKey, TValue>(out TValue value) where TKey : struct, GenericStorageKey<TValue>
    {
        var key = typeof(TKey);
        if (storage.TryGetValue(key, out var rawValue))
        {
            value = (TValue)rawValue;
            return true;
        }

        value = default;
        return false;
    }
}

public interface GenericStorageKey<TValue> {}