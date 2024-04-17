using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain.Core;
using System.ComponentModel;
using System.Diagnostics;

namespace IncrementalCivilization.Domain;

[DebuggerDisplay("Name = {ResourceName}, ResourceValue = {Value}, CostValue = {CostValue}")]
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
        _resource.PropertyChanged += ResourcePropertyChanged;

        CostValue = value;
    }

    private void ResourcePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged("Resource" + e.PropertyName); // Forward change notification (prepended with Resource)
        CanAfford = ResourceValue >= CostValue;
    }

    public void SubtractCostFromResource()
    {
        _resource.Sub(CostValue, skipRateUpdate: true);
    }

    public bool IsOverThreshold(double threshold)
    {
        return _resource.Value >= CostValue*threshold;
    }

    public void Clear()
    {
        _resource.PropertyChanged -= ResourcePropertyChanged;
    }
}
