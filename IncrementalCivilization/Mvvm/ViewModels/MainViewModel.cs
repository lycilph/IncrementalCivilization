using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Mvvm.Services;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    private INavigationService _navigationService;

    public ObservableCollection<IPageViewModel> Pages { get; private set; } = new ObservableCollection<IPageViewModel>();

    [ObservableProperty]
    private IPageViewModel? _currentPage;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        Pages.Add(new HomePageViewModel());
        Pages.Add(new ResearchPageViewModel());
        Pages.Add(new TimePageViewModel());

        CurrentPage = Pages.First();
    }

    [RelayCommand]
    private void ShowSettings()
    {
        _navigationService.NavigateTo<ISettingsViewModel>();
    }

    [RelayCommand]
    private void ShowPage(IPageViewModel page)
    {
        CurrentPage = page;
    }
}
