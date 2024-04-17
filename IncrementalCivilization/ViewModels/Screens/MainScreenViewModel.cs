using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class MainScreenViewModel(IEnumerable<IPageViewModel> pages, INavigationService navigationService, Game game) 
    : ViewModelBase, IMainScreenViewModel
{
    public ObservableCollection<IPageViewModel> Pages { get; private set; } = new ObservableCollection<IPageViewModel>(pages);

    [ObservableProperty]
    private IPageViewModel? _currentPage;
    
    [ObservableProperty]
    private string _statusBarModeMessage = string.Empty;

    public Time Time { get => game.Time; }

    public override void Initialize()
    {
        base.Initialize();

        Settings.Default.PropertyChanged += (s,e) => UpdateStatusBarModeMessage();
        UpdateStatusBarModeMessage();

        navigationService.NavigateToPage(Pages.First());
    }

    private void UpdateStatusBarModeMessage()
    {
        StatusBarModeMessage = Settings.Default.DebugMode ? "Debug Mode" : string.Empty;
    }

    [RelayCommand]
    private void ShowSettings()
    {
        navigationService.NavigateToScreen<ISettingsScreenViewModel>();
    }
}
