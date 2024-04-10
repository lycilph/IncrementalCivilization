using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public enum ResourceType { Population, Food, Wood, Science }

public partial class Resource : ObservableObject, ITypedItem<ResourceType>
{
    public ResourceType Type { get; private set; }

    [ObservableProperty]
    private bool active = false;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private double _value = 0;

    [ObservableProperty]
    private double _maximum = 0;

    public Resource(ResourceType type, string name, double max)
    {
        Type = type;
        Name = name;
        Maximum = max;
    }

    public Resource(ResourceType type, double max) : this(type, type.ToString(), max) { }

    public Resource(ResourceType type) : this(type, type.ToString(), 0.0) { }
}
