using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sandbox.Domain;
using System.Collections.ObjectModel;

namespace Sandbox.ViewModels;

public partial class BuildingItemViewModel : ObservableObject
{
    public BuildingItem Item { get; private set; }
    public ObservableCollection<CostItemViewModel> CostItems { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyCommand))]
    public bool canBuy = false;

    public BuildingItemViewModel(BuildingItem item)
    {
        Item = item;

        CostItems = new ObservableCollection<CostItemViewModel>(item.Cost.Select(c => new CostItemViewModel(c)));
        foreach (var i in CostItems)
            i.PropertyChanged += (s, e) => CanBuy = CostItems.All(i => i.CanAfford);
    }

    [RelayCommand(CanExecute = nameof(CanBuy))]
    private void Buy()
    {
        Item.Buy();
    }
}
