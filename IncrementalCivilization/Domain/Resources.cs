using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public enum ResourceType { People, Food, Wood, Mineral, Iron };

public partial class Resource : ObservableObject
{
    public ResourceType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool ShowRate { get; set; } = true;

    [ObservableProperty]
    private double _amount = 0;

    [ObservableProperty]
    private double _threshold = 0; // This can be used as both a maximum (for storage) or as a price (in buildings etc.)

    [ObservableProperty]
    private double _rate = 0;

    [ObservableProperty]
    private bool _overThreshold = false;

    [ObservableProperty]
    private bool _active = false;

    public Resource(ResourceType type, double amount, double threshold)
    {
        Type = type;
        Name = type.ToString() ?? string.Empty;
        Amount = amount;
        Threshold = threshold;
    }

    public Resource(ResourceType type, double amount = 0) : this(type, amount, 0) { }

    partial void OnAmountChanged(double value)
    {
        if (!Active && Amount > 0)
            Active = true;

        OverThreshold = Amount >= Threshold;
    }

    partial void OnAmountChanging(double oldValue, double newValue)
    {
        Rate = newValue - oldValue;
    }

    public void AddAndLimit(double v)
    {
        Amount += v;
        Amount = Amount.Clamp(0, Threshold);
    }

    public void Zero()
    {
        Amount = 0;
        Rate = 0;
    }

    public void Limit()
    {
        Amount = Amount.Clamp(0, Threshold);
    }
}

public class ResourceBundle : IEnumerable<Resource>
{
    private readonly Dictionary<ResourceType, Resource> resources = [];

    public Dictionary<ResourceType, Resource>.KeyCollection Keys { get => resources.Keys; }

    public ResourceBundle Add(Resource resource)
    {
        resources.Add(resource.Type, resource);
        return this;
    }

    public ResourceBundle Add(ResourceType type, double amount = 0)
    {
        resources.Add(type, new Resource(type, amount));
        return this;
    }

    public ResourceBundle Add(ResourceType type, double amount, double threshold)
    {
        resources.Add(type, new Resource(type, amount, threshold));
        return this;
    }

    public Resource this[ResourceType type] => resources[type];

    public IEnumerator<Resource> GetEnumerator()
    {
        foreach (var r in resources.Values)
            yield return r;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Resource>)this).GetEnumerator();

    public static ResourceBundle AllResources()
    {
        var bundle = new ResourceBundle();
        foreach (var r in Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>())
            bundle.Add(new Resource(r));
        return bundle;
    }

    public static ResourceBundle Single(ResourceType type, double amount = 0, double threshold = 0)
    {
        return
        [
            new Resource(type, amount, threshold)
        ];
    }
}