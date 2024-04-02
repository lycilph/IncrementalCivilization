using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;

namespace IncrementalCivilization.ViewModels;

public partial class SettingsViewModel(INavigationService navigationService, ILogger<SettingsViewModel> logger) : ViewModelBase(logger), ISettingsViewModel
{
    [RelayCommand]
    private void ShowMain()
    {
        navigationService.NavigateToMain();
    }
}
