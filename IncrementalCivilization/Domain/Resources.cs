using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;

namespace IncrementalCivilization.Domain;

public enum ResourceType { Food, Wood, Mineral, Iron };

public partial class Resource : ObservableObject
{
    public ResourceType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;

    [ObservableProperty]
    private double _amount = 0;

    [ObservableProperty]
    private bool _active = false;

    public Resource(ResourceType type, double amount = 0)
    {
        Type = type;
        Name = type.ToString() ?? string.Empty;
        Amount = amount;
    }

    public void Add(double val) 
    {
        Amount += val;
        if (Amount > 0)
            Active = true;
    }
}

public class ResourceBundle : IEnumerable<Resource>
{
    public Dictionary<ResourceType, Resource> Resources { get; private set; } = [];

    public ResourceBundle Add(Resource resource)
    {
        Resources.Add(resource.Type, resource);
        return this;
    }

    public ResourceBundle Add(ResourceType type, double amount = 0)
    {
        Resources.Add(type, new Resource(type, amount));
        return this;
    }

    public Resource Get(ResourceType type) => Resources[type];

    public IEnumerable<Resource> GetAll() => Resources.Values;

    public Resource this[ResourceType type] => Resources[type];

    public IEnumerator<Resource> GetEnumerator()
    {
        foreach (var r in Resources.Values)
            yield return r;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Resource>)this).GetEnumerator();

    public static ResourceBundle operator -(ResourceBundle a, ResourceBundle b)
    {
        foreach (var key in b.Resources.Keys)
        {
            var a_item = a.Resources[key];
            var b_item = b.Resources[key];

            if (a_item != null)
                a_item.Amount -= b_item.Amount;
        }
        return a;
    }

    public static ResourceBundle operator *(ResourceBundle a, double v)
    {
        foreach (var i in a.Resources)
            i.Value.Amount *= v;
        return a;
    }

    public static bool operator >(ResourceBundle a, ResourceBundle b)
    {
        foreach (var key in b.Resources.Keys)
        {
            var a_item = a.Resources[key];
            var b_item = b.Resources[key];

            if (a_item == null || a_item.Amount < b_item.Amount)
                return false;
        }
        return true;
    }

    public static bool operator <(ResourceBundle a, ResourceBundle b)
    {
        foreach (var key in b.Resources.Keys)
        {
            var a_item = a.Resources[key];
            var b_item = b.Resources[key];

            if (a_item == null || a_item.Amount > b_item.Amount)
                return false;
        }
        return true;
    }

    public static ResourceBundle AllResources()
    {
        return
        [
            new Resource(ResourceType.Food),
            new Resource(ResourceType.Wood),
            new Resource(ResourceType.Mineral),
            new Resource(ResourceType.Iron)
        ];
    }

    public static ResourceBundle SingleResourceBundle(ResourceType type, double amount = 0)
    {
        return
        [
            new Resource(type, amount)
        ];
    }
}