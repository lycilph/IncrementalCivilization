using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Mvvm.Services;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly INavigationService _navigationService;

    public ObservableCollection<IPageViewModel> Pages { get; private set; } = new ObservableCollection<IPageViewModel>();

    [ObservableProperty]
    private IPageViewModel? _currentPage;

    [ObservableProperty]
    private IGame _game;

    public ObservableCollection<string> Log { get; private set; } = [];

    public MainViewModel(INavigationService navigationService, IGame game, IEnumerable<IPageViewModel> pages)
    {
        _navigationService = navigationService;
        _game = game;

        foreach (var pageViewModel in pages)
            Pages.Add(pageViewModel);

        ShowPage(Pages.First());

        var i = 0;
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
        timer.Tick += (s,e) => { Log.Add($"Test {i++}"); };
        timer.Start();
    }

    [RelayCommand]
    private void ShowSettings()
    {
        _navigationService.NavigateTo<ISettingsViewModel>();
    }

    [RelayCommand]
    private void ShowPage(IPageViewModel page)
    {
        if (!page.Initialized)
            page.Initialize();

        CurrentPage = page;
    }
}
