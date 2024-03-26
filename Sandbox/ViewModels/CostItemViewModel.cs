using CommunityToolkit.Mvvm.ComponentModel;
using Sandbox.Domain;

namespace Sandbox.ViewModels;

public partial class CostItemViewModel : ObservableObject
{
    private CostItem costItem;
   
    public string Name { get => costItem.Name; }
    public double Value { get => costItem.Value; }
    public double Cost { get => costItem.Cost; }

    [ObservableProperty]
    private bool canAfford = false;

    public CostItemViewModel(CostItem costItem)
    {
        this.costItem = costItem;
        this.costItem.PropertyChanged += (s, e) => 
        {
            OnPropertyChanged(e.PropertyName);
            CanAfford = Value >= Cost;
        };
    }
}
