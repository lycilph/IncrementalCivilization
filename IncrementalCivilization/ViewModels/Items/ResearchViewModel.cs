using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels.Items;

public partial class ResearchViewModel : ObservableObject
{
    public ResearchItem Item { get; private set; }
    public ObservableCollection<CostViewModel> Cost { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyCommand))]
    public bool canBuy = false;

    public ResearchViewModel(ResearchItem item)
    {
        Item = item;
        Cost = new ObservableCollection<CostViewModel>(item.Cost.Select(c => new CostViewModel(c)));

        foreach (var i in item.Cost)
            i.PropertyChanged += (s, e) => CanBuy = !Item.IsBought && Cost.All(i => i.CanAfford);
    }

    [RelayCommand(CanExecute = nameof(CanBuy))]
    private void Buy()
    {
        Item.Buy();
    }
}
