using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Pages;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels;

public partial class MainViewModel : ViewModelBase, IMainViewModel
{
    private readonly INavigationService _navigationService;

    public ObservableCollection<IPageViewModel> Pages { get; private set; }

    [ObservableProperty]
    private IPageViewModel? _currentPage;

    public Game Game { get; private set; }

    public IMessageLog Messages { get; private set; }

    public MainViewModel(Game game, IEnumerable<IPageViewModel> pages, INavigationService navigationService, IMessageLog messages, ILogger<MainViewModel> logger) : base(logger)
    {
        Pages = new ObservableCollection<IPageViewModel>(pages);
        Game = game;
        Messages = messages;
        _navigationService = navigationService;
    }

    public override void Initialize()
    {
        base.Initialize();
        _navigationService.NavigateToPage(Pages.First());
    }

    [RelayCommand]
    private void ShowSettings()
    {
        _navigationService.NavigateToSettings();
    }
}
