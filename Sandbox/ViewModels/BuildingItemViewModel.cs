using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sandbox.Domain;
using System.Collections.ObjectModel;

namespace Sandbox.ViewModels;

public partial class BuildingItemViewModel : ObservableObject
{
    public BuildingItem Item { get; private set; }
    public ObservableCollection<CostItemViewModel> CostItems { get; private set; } = [];

    public BuildingItemViewModel(BuildingItem item, ResourcesBundle resources)
    {
        Item = item;

        foreach (var cost_res in Item.Cost)
            CostItems.Add(new CostItemViewModel(cost_res, resources[cost_res.Type]));
    }

    //private void Update()
    //{
    //    foreach (var cost_res in Item.Cost)
    //        cost_res.Value = Resources[cost_res.Type].Value;
    //}

    [RelayCommand]
    private void Buy()
    {
        Item.Count++;
    }
}
