using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using Microsoft.Extensions.Logging;

namespace IncrementalCivilization.ViewModels;

public partial class ShellViewModel(Game game, ILogger<ShellViewModel> logger) : ViewModelBase(logger), IShellViewModel
{
    [ObservableProperty]
    private IViewModel? _current;

    [RelayCommand]
    private void ToggleDebugging()
    {
        _logger.LogInformation("Toggling debugging");
        game.ToogleDebugging();
    }
}
