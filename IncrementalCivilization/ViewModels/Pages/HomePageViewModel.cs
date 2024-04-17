using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel : PageViewModelBase, IHomePageViewModel
{
    private readonly ResourcesViewModel _resources;
    private readonly DebugViewModel _debugViewModel;
    private readonly Game _game;

    public ResourcesViewModel ResourcesVM { get => _resources; }
    public DebugViewModel DebugVM { get => _debugViewModel; }

    public bool DebugMode
    {
        get => Settings.Default.DebugMode;
        set => SetProperty(Settings.Default.DebugMode, value, Settings.Default, (o, v) => o.DebugMode = v);
    }

    public HomePageViewModel(INavigationService navigationService, ResourcesViewModel resources, DebugViewModel debugViewModel, Game game) : base(navigationService, "Home", SymbolRegular.Home24)
    {
        _resources = resources;
        _debugViewModel = debugViewModel;
        _game = game;

        Enabled = true;
        Settings.Default.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName); // Forward change notifications to VM
    }

    [RelayCommand]
    private void GatherFood()
    {
        _game.Resources.Food.Add(1, skipRateUpdate: true);
    }
}
