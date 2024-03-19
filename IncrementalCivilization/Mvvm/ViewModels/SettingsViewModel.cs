using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Mvvm.Services;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class SettingsViewModel : ObservableObject, ISettingsViewModel
{
    private INavigationService _navigationService;

    public SettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void ShowMain()
    {
        _navigationService.NavigateTo<IMainViewModel>();
    }
}
