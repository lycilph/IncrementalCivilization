using CommunityToolkit.Mvvm.ComponentModel;

namespace Sandbox.Domain;

public class CostsBundle : ItemsBundle<ResourceItemType, CostItem> { }

public partial class CostItem : ObservableObject, ITypedItem<ResourceItemType>
{
    private readonly ResourceItem resourceItem;

    public ResourceItemType Type { get => resourceItem.Type; }
    public string Name { get => resourceItem.Name; }
    public double Value { get => resourceItem.Value; }

    [ObservableProperty]
    private double cost = 0.0;

    public CostItem(ResourceItem resourceItem, double cost)
    {
        this.resourceItem = resourceItem;
        this.resourceItem.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);

        Cost = cost;
    }

    public void SubtractCostFromResources()
    {
        resourceItem.Subtract(Cost);
    }
}
