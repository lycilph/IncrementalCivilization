using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly Game game;

    [ObservableProperty]
    private bool _debugMode = false;

    public ResourcesViewModel Resources { get; private set; }
    public IEnumerable<Building> Buildings { get => game.Buildings; }

    public HomePageViewModel(INavigationService navigationService, ResourcesViewModel resources, Game game) : base(navigationService, "Home", SymbolRegular.Home24)
    {
        this.game = game;
        Resources = resources;

        Enabled = true;
        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMode();
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateDebugMode();
    }

    private void UpdateDebugMode()
    {
        DebugMode = Settings.Default.Debug;
    }

    [RelayCommand]
    private void GatherFood()
    {
        game.Resources.Food.Add(1);
    }

    [RelayCommand]
    private void AddField()
    {
        game.Buildings.Field.Buy();
    }

    [RelayCommand]
    private void EnableAllPages()
    {
        StrongReferenceMessenger.Default.Send(EnablePageMessage.All());
    }
}
