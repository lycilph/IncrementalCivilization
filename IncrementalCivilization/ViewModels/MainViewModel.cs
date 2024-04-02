using CommunityToolkit.Mvvm.ComponentModel;
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

    public MainViewModel(IEnumerable<IPageViewModel> pages, INavigationService navigationService, ILogger<MainViewModel> logger) : base(logger)
    {
        Pages = new ObservableCollection<IPageViewModel>(pages);
        _navigationService = navigationService;
    }

    public override void Initialize()
    {
        base.Initialize();
        _navigationService.NavigateToPage(Pages.First());
    }
}
