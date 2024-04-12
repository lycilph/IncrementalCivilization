using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IncrementalCivilization.Domain;

public partial class Improvement : ObservableObject
{
    public CostsBundle Cost { get; set; } = [];
    public Action BuyAction { get; set; } = () => { };

    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private bool _isBought = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyCommand))]
    public bool canBuy = false;

    public Improvement(string name = "", string description = "")
    {
        Name = name;
        Description = description;

        Cost.PropertyChanged += (s,e) => CanBuy = !IsBought && Cost.All(c => c.CanAfford);
    }

    [RelayCommand(CanExecute = nameof(CanBuy))]
    public void Buy()
    {
        IsBought = true;
        Cost.SubtractCostsFromResources();
        BuyAction();
    }
}
