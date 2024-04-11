using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Collections.Specialized;

namespace Sandbox;

public interface ITypedItem<T> { T Type { get; } }

public class Bundle<TType, TItem> : INotifyCollectionChanged, IEnumerable<TItem> where TType : notnull where TItem : ITypedItem<TType>
{
    protected readonly Dictionary<TType, TItem> items = [];

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

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
        return this;
    }

    public Bundle<TType, TItem> Add(params TItem[] item)
    {
        foreach (var i in item)
            items.Add(i.Type, i);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        return this;
    }

    public virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this, args);
    }
}

public enum ResourceType { Population, Food, Wood, Science }

public partial class Resource(ResourceType type, double value) : ObservableObject, ITypedItem<ResourceType>
{
    public ResourceType Type { get; private set; } = type;
    [ObservableProperty]
    private string _name = type.ToString();
    [ObservableProperty]
    private double _value = value;
}

public class Building : ObservableObject
{
    public Bundle<ResourceType, Resource> Resources { get; private set; } = [];
    public double Total { get; set; }

    public Building()
    {
        Resources.CollectionChanged += Resources_CollectionChanged;
    }

    private void Resources_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            foreach (var i in e.NewItems)
                if (i is Resource r && r != null)
                    r.PropertyChanged += (s, e) => UpdateSum();
        UpdateSum();
    }

    private void UpdateSum()
    {
        Total = Resources.Sum(r => r.Value);
    }
}

internal class Program
{
    static void Main(string[] _)
    {
        var building = new Building();
        building.Resources.Add(new Resource(ResourceType.Food, 10));
        building.Resources.Add(new Resource(ResourceType.Wood, 0));
        Console.WriteLine($"Total: {building.Total}");

        building.Resources[ResourceType.Wood].Value += 5;
        Console.WriteLine($"Total: {building.Total}");

        building.Resources.Add(new Resource(ResourceType.Science, 3));
        Console.WriteLine($"Total: {building.Total}");

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }
}
