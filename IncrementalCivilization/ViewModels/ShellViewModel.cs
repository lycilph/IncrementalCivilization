using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Properties;
using IncrementalCivilization.ViewModels.Shared;
using NLog;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace IncrementalCivilization.ViewModels;

public partial class ShellViewModel(ISnackbarService snackbarService) : ViewModelBase, IShellViewModel
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    [ObservableProperty]
    private IViewModel? _currentScreen;

    [RelayCommand]
    private void ToggleDebug()
    {
        Settings.Default.Debug = !Settings.Default.Debug;

        var msg = $"Debug toggled (current value = {Settings.Default.Debug})";
        logger.Debug(msg);
        snackbarService.Show("Info", msg, Wpf.Ui.Controls.ControlAppearance.Info);
    }
}
