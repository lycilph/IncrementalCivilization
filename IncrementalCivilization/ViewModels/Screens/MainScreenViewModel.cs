using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class MainScreenViewModel : ViewModelBase, IMainScreenViewModel
{
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private string _debugMessage = string.Empty;

    public ObservableCollection<IPageViewModel> Pages { get; private set; }

    [ObservableProperty]
    private IPageViewModel? _currentPage;

    public MainScreenViewModel(INavigationService navigationService, IEnumerable<IPageViewModel> pages)
    {
        Pages = new ObservableCollection<IPageViewModel>(pages);

        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMessage();
        this.navigationService = navigationService;
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateDebugMessage();
        navigationService.NavigateToPage(Pages.First());
    }

    private void UpdateDebugMessage()
    {
        DebugMessage = Settings.Default.Debug ? "Debugging" : string.Empty;
    }
}
