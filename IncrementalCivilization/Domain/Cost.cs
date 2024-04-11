using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public partial class Cost : ObservableObject, ITypedItem<ResourceType>
{
    private readonly Resource _resource;

    public ResourceType Type { get => _resource.Type; }
    public string ResourceName { get => _resource.Name; }
    public double ResourceValue { get => _resource.Value; }

    [ObservableProperty]
    private double _costValue = 0.0;

    [ObservableProperty]
    private bool _canAfford = false;

    public Cost(Resource resource, double value)
    {
        _resource = resource;
        _resource.PropertyChanged += (s, e) =>
        {
            OnPropertyChanged("Resource" + e.PropertyName); // Forward change notification (prepended with Resource)
            CanAfford = ResourceValue >= CostValue;
        };

        CostValue = value;
    }

    public void SubtractCostFromResource()
    {
        _resource.Sub(CostValue, skipRateUpdate: true);
    }
}
