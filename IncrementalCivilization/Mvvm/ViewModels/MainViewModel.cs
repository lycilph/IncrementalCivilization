using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Mvvm.Services;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    private INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void ShowSettings()
    {
        _navigationService.NavigateTo<ISettingsViewModel>();
    }
}
