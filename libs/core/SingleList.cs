namespace Cusco;

public sealed class SingleList<T> where T: class
{
    private T firstItem;
    private List<T> furtherItems;

    public void Add(T item)
    {
        if (Interlocked.CompareExchange(ref firstItem, item, null) == null)
            return;

        if (Volatile.Read(ref furtherItems) == null)
            Interlocked.CompareExchange(ref furtherItems, new List<T>(), null);
        
        Volatile.Read(ref furtherItems).Add(item);
    }

    public void Clear()
    {
        Interlocked.Exchange(ref firstItem, null);
        Volatile.Read(ref furtherItems)?.Clear();
    }

    public void ForEach(Action<T> block)
    {
        var item = Volatile.Read(ref firstItem);
        if (null == item)
            return;

        block(item);

        var otherItems = Volatile.Read(ref furtherItems);
        if (null == otherItems)
            return;

        foreach (var otherItem in otherItems)
            block(item);
    }
}