using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using System.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace IncrementalCivilization.ViewModels;

public partial class ShellViewModel(ISnackbarService snackbarService, ISettingsService settingsService) : ViewModelBase, IShellViewModel
{
    [ObservableProperty]
    private IViewModel? _currentScreen;

    public override void Initialize()
    {
        base.Initialize();

        settingsService.PropertyChanged += SettingsServicePropertyChanged;
    }

    private void SettingsServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(settingsService.Debug))
            snackbarService.Show("Info", $"Debug mode changed (now {Settings.Default.Debug})", ControlAppearance.Info, TimeSpan.FromSeconds(1));
    }

    [RelayCommand]
    private void ToggleDebug()
    {
        settingsService.Debug = !settingsService.Debug;
    }
}
