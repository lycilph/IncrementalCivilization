using CommunityToolkit.Mvvm.ComponentModel;
using Sandbox.Domain;

namespace Sandbox.ViewModels;

public class CostItemViewModel : ObservableObject
{
    private ResourceItem cost;
    private ResourceItem resource;
   
    public string Name { get => cost.Name; }
    public double Value { get => resource.Value; }
    public double Threshold { get => cost.Threshold; }

    public CostItemViewModel(ResourceItem cost, ResourceItem resource)
    {
        this.cost = cost;
        this.resource = resource;

        resource.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
    }
}
