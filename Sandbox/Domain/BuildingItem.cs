using CommunityToolkit.Mvvm.ComponentModel;

namespace Sandbox.Domain;

public enum BuildingItemType { Field, Mine, Hut };

public class BuildingsBundle : ItemsBundle<BuildingItemType, BuildingItem> { }

public partial class BuildingItem : ObservableObject, ITypedItem<BuildingItemType>
{
    public BuildingItemType Type { get; private set; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private double count = 0;

    public BuildingItem(BuildingItemType type, string name)
    {
        Type = type;
        this.name = name;
    }

    public BuildingItem(BuildingItemType type) : this(type, type.ToString()) { }
}
