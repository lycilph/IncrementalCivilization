using CommunityToolkit.Mvvm.ComponentModel;
using Sandbox.Domain;

namespace Sandbox.ViewModels;

public partial class CostItemViewModel : ObservableObject
{
    public CostItem Item { get; private set; }

    [ObservableProperty]
    private bool canAfford = false;

    public CostItemViewModel(CostItem item)
    {
        Item = item;
        this.Item.PropertyChanged += (s, e) =>
        {
            OnPropertyChanged(e.PropertyName);
            CanAfford = Item.Value >= Item.Cost;
        };
    }
}
