using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class MainScreenViewModel : ViewModelBase, IMainScreenViewModel
{
    private readonly INavigationService navigationService;
    private readonly Game game;

    [ObservableProperty]
    private string _statusBarModeMessage = string.Empty;

    public ObservableCollection<IPageViewModel> Pages { get; private set; }

    [ObservableProperty]
    private IPageViewModel? _currentPage;

    public Time Time { get => game.Time; }
    public DispatcherTimer Timer { get => game.Timer; }
    public IMessageLog Messages { get; private set; }

    public MainScreenViewModel(INavigationService navigationService, IEnumerable<IPageViewModel> pages, Game game, IMessageLog messages)
    {
        this.navigationService = navigationService;
        this.game = game;
        Pages = new ObservableCollection<IPageViewModel>(pages);
        Messages = messages;
    }

    public override void Initialize()
    {
        base.Initialize();

        Settings.Default.PropertyChanged += (s, e) => UpdateStatusBarModeMessage();
        UpdateStatusBarModeMessage();

        navigationService.NavigateToPage(Pages.First());
    }

    private void UpdateStatusBarModeMessage()
    {
        StatusBarModeMessage = Settings.Default.Debug ? "Debugging" : string.Empty;
    }

    [RelayCommand]
    private void ShowSettings()
    {
        navigationService.NavigateToScreen<ISettingsScreenViewModel>();
    }
}
