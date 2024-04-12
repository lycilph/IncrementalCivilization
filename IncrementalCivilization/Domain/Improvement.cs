using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;

public partial class Improvement(string name = "", string description = "") : ObservableObject
{
    public CostsBundle Cost { get; set; } = [];
    public Action BuyAction { get; set; } = () => { };

    [ObservableProperty]
    private string _name = name;
    [ObservableProperty]
    private string _description = description;
    [ObservableProperty]
    private bool _isBought = false;

    public void Buy()
    {
        IsBought = true;
        Cost.SubtractCostsFromResources();
        BuyAction();
    }
}
