using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Properties;
using IncrementalCivilization.ViewModels.Core;
using System.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace IncrementalCivilization.ViewModels;

public partial class ShellViewModel(ISnackbarService snackbarService) : ViewModelBase, IShellViewModel
{
    [ObservableProperty]
    private IViewModel? _currentScreen;

    public override void Initialize()
    {
        base.Initialize();

        Settings.Default.PropertyChanged += SettingsServicePropertyChanged;
    }

    private void SettingsServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Settings.Default.DebugMode))
            snackbarService.Show("Info", $"Debug mode changed (now {Settings.Default.DebugMode})", ControlAppearance.Info, TimeSpan.FromSeconds(1));
    }

    [RelayCommand]
    private void ToggleDebug()
    {
        Settings.Default.DebugMode = !Settings.Default.DebugMode;
    }
}
