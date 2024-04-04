using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;

public enum BuildingItemType { Field, Hut };

public partial class BuildingItem : ObservableObject, ITypedItem<BuildingItemType>
{
    public BuildingItemType Type { get; private set; }

    public CostsBundle Cost { get; set; } = [];
    public Action BuyAction { get; set; } = () => { };

    [ObservableProperty]
    private double costIncrease = 1.15;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int count = 0;

    public BuildingItem(BuildingItemType type, string name)
    {
        Type = type;
        this.name = name;
    }

    public BuildingItem(BuildingItemType type) : this(type, type.ToString()) { }

    public void Buy()
    {
        Count++;
        foreach (var item in Cost)
        {
            item.SubtractCostFromResources();
            item.Cost *= CostIncrease;
        }
        BuyAction();
    }
}
