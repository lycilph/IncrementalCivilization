using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class SettingsScreenViewModel : ViewModelBase, ISettingsScreenViewModel
{
    private readonly INavigationService navigationService;

    public bool DebugMode
    {
        get => Settings.Default.DebugMode;
        set => SetProperty(Settings.Default.DebugMode, value, Settings.Default, (o, v) => o.DebugMode = v);
    }

    public SettingsScreenViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;

        Settings.Default.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName); // Forward change notifications to VM
    }

    [RelayCommand]
    private void ShowMain()
    {
        navigationService.NavigateToScreen<IMainScreenViewModel>();
    }
}
