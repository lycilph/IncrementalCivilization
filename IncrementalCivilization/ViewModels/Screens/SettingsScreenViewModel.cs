using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class SettingsScreenViewModel(INavigationService navigationService) : ViewModelBase, ISettingsScreenViewModel
{
    [ObservableProperty]
    private bool _debugMode = false;

    public override void Initialize()
    {
        base.Initialize();

        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMode();
        UpdateDebugMode();
    }

    partial void OnDebugModeChanged(bool value)
    {
        Settings.Default.Debug = DebugMode;
    }

    private void UpdateDebugMode()
    {
        DebugMode = Settings.Default.Debug;
    }

    [RelayCommand]
    private void ShowMain()
    {
        navigationService.NavigateToScreen<IMainScreenViewModel>();
    }
}
