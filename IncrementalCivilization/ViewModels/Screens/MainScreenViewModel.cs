using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class MainScreenViewModel(IEnumerable<IPageViewModel> pages, INavigationService navigationService, ISettingsService settingsService, Game game) 
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

        settingsService.PropertyChanged += (s,e) => UpdateStatusBarModeMessage();
        UpdateStatusBarModeMessage();

        navigationService.NavigateToPage(Pages.First());
    }

    private void UpdateStatusBarModeMessage()
    {
        StatusBarModeMessage = settingsService.Debug ? "Debug Mode" : string.Empty;
    }
}
