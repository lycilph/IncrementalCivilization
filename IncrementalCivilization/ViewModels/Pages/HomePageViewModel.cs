using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Shared;
using System.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel : PageViewModelBase, IHomePageViewModel
{
    private readonly ResourcesViewModel _resources;
    private readonly DebugViewModel _debugViewModel;
    private readonly Game _game;

    public ResourcesViewModel ResourcesVM { get => _resources; }
    public DebugViewModel DebugVM { get => _debugViewModel; }
    public BuildingsBundle Buildings { get => _game.Buildings; }
    public Capabilities Capabilities { get => _game.Capabilities; }

    public bool DebugMode
    {
        get => Settings.Default.DebugMode;
        set => SetProperty(Settings.Default.DebugMode, value, Settings.Default, (o, v) => o.DebugMode = v);
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RefineFoodCommand))]
    private bool _canRefineFood = false;

    public HomePageViewModel(INavigationService navigationService, ResourcesViewModel resources, DebugViewModel debugViewModel, Game game) : base(navigationService, "Home", SymbolRegular.Home24)
    {
        _resources = resources;
        _debugViewModel = debugViewModel;
        _game = game;

        Enabled = true;
        Settings.Default.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName); // Forward change notifications to VM
    }

    public override void Activate()
    {
        base.Activate();

        _game.Resources.Food.PropertyChanged += FoodPropertyChanged;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        _game.Resources.Food.PropertyChanged -= FoodPropertyChanged;
    }

    private void FoodPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        CanRefineFood = _game.Resources.Food.Value >= 100;
    }

    [RelayCommand]
    private void GatherFood()
    {
        _game.Resources.Food.Add(1, skipRateUpdate: true);
    }

    [RelayCommand(CanExecute = nameof(CanRefineFood))]
    private void RefineFood()
    {
        _game.Resources.Wood.Add(1, skipRateUpdate: true);
        _game.Resources.Food.Sub(100, skipRateUpdate: true);
    }
}
