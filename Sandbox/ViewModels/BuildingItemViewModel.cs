using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sandbox.Domain;

namespace Sandbox.ViewModels;

public partial class BuildingItemViewModel : ObservableObject
{
    public BuildingItem Item { get; private set; }

    public BuildingItemViewModel(BuildingItem item)
    {
        Item = item;
    }

    [RelayCommand]
    private void Buy()
    {
        Item.Count++;
    }
}
