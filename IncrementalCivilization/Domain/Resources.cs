using CommunityToolkit.Mvvm.ComponentModel;

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
        Name = type.ToString() ?? string.Empty;
        Type = type;
        Amount = amount;
    }

    public void Add(double val) 
    {
        Amount += val;
        if (Amount > 0)
            Active = true;
    }
}

public class ResourceBundle
{
    public Dictionary<ResourceType, Resource> Resources { get; private set; } = [];

    public void Add(Resource resource) => Resources.Add(resource.Type, resource);

    public Resource Get(ResourceType type) => Resources[type];

    public IEnumerable<Resource> GetAll() => Resources.Values;

    public Resource this[ResourceType type] => Resources[type];

    public static ResourceBundle AllResources()
    {
        var bundle = new ResourceBundle();

        bundle.Add(new Resource(ResourceType.Food));
        bundle.Add(new Resource(ResourceType.Wood));
        bundle.Add(new Resource(ResourceType.Mineral));
        bundle.Add(new Resource(ResourceType.Iron));

        return bundle;
    }
}