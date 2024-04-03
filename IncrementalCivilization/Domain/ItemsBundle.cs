using System.Collections;

namespace IncrementalCivilization.Domain;

public class ItemsBundle<TType, TItem> : IEnumerable<TItem> where TType : notnull where TItem : ITypedItem<TType>
{
    private readonly Dictionary<TType, TItem> items = [];

    public TItem this[TType type] => items[type];

    public IEnumerator<TItem> GetEnumerator()
    {
        foreach (var i in items.Values)
            yield return i;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TItem>)this).GetEnumerator();

    public ItemsBundle<TType, TItem> Add(TItem item)
    {
        items.Add(item.Type, item);
        return this;
    }
}
