using CommunityToolkit.Mvvm.ComponentModel;
using Sandbox.Domain;
using System.Collections.ObjectModel;

namespace Sandbox.ViewModels;

public partial class BuildingsBundleViewModel : ObservableObject
{
    public ObservableCollection<BuildingItemViewModel> Items { get; private set; }

    public BuildingsBundleViewModel(IEnumerable<BuildingItem> items, ResourcesBundle resources)
    {
        Items = new ObservableCollection<BuildingItemViewModel>(items.Select(x => new BuildingItemViewModel(x, resources)));
    }
}
