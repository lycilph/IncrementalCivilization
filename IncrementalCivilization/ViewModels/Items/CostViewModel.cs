using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;

namespace IncrementalCivilization.ViewModels.Items;

public partial class CostViewModel : ObservableObject
{
    public CostItem Item { get; private set; }

    [ObservableProperty]
    private bool canAfford = false;

    public CostViewModel(CostItem item)
    {
        Item = item;
        Item.PropertyChanged += (s, e) =>
        {
            OnPropertyChanged(e.PropertyName); // Forward the change notification
            CanAfford = Item.Value >= Item.Cost;
        };
    }
}
