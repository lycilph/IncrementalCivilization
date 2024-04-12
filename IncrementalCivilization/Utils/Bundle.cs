using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace IncrementalCivilization.Utils;

public class Bundle<TType, TItem> : INotifyCollectionChanged, INotifyPropertyChanged, IEnumerable<TItem> where TType : notnull where TItem : ITypedItem<TType>, INotifyPropertyChanged
{
    protected readonly Dictionary<TType, TItem> items = [];

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public TItem this[TType type] => items[type];

    public IEnumerator<TItem> GetEnumerator()
    {
        foreach (var i in items.Values)
            yield return i;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TItem>)this).GetEnumerator();

    public Bundle<TType, TItem> Add(TItem item)
    {
        items.Add(item.Type, item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        item.PropertyChanged += (s,e) => OnPropertyChanged(e.PropertyName!);
        return this;
    }

    public Bundle<TType, TItem> Add(params TItem[] item)
    {
        foreach (var i in item)
        { 
            items.Add(i.Type, i);
            i.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName!);
        }
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        return this;
    }

    public virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this, args);
    }

    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
