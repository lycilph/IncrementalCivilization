using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Shared;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class UpgradesPageViewModel : PageViewModelBase
{
    private readonly Game _game;
    private readonly ResourcesViewModel _resources;

    public ResourcesViewModel ResourcesVM { get => _resources; }
    public ObservableCollection<Improvement> Unlocked { get => _game.Upgrades.Unlocked; }
    public ObservableCollection<Improvement> Bought { get => _game.Upgrades.Bought; }

    [ObservableProperty]
    private bool _hideBoughtUpgrades = false;

    public UpgradesPageViewModel(INavigationService navigationService, ResourcesViewModel resources, Game game) : base(navigationService, "Upgrades", SymbolRegular.Star24)
    {
        _game = game;
        _resources = resources;
        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.UpgradesPageEnabled;
    }
}
