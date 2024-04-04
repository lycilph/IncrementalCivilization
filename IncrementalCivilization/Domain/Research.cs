using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public partial class ResearchItem : ObservableObject
{
    public CostsBundle Cost { get; set; } = [];
    public Action BuyAction { get; set; } = () => { };

    [ObservableProperty]
    private string name = string.Empty;
    
    [ObservableProperty]
    private bool isBought = false;

    public void Buy()
    {
        IsBought = true;
        Cost.Apply(i => i.SubtractCostFromResources());
        BuyAction();
    }
}
