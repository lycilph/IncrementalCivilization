using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels;

public partial class SettingsViewModel(ISettingsService settingsService, INavigationService navigationService, ILogger<SettingsViewModel> logger) : ViewModelBase(logger), ISettingsViewModel
{
    public ObservableCollection<Option> Options { get => settingsService.Options; }

    [RelayCommand]
    private void ShowMain()
    {
        navigationService.NavigateToMain();
    }
}
