using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public partial class ImprovementItem(string name = "", string description = "") : ObservableObject
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
        Cost.Apply(i => i.SubtractCostFromResources());
        BuyAction();
    }
}
