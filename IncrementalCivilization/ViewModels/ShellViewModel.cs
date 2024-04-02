using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace IncrementalCivilization.ViewModels;

public partial class ShellViewModel : ObservableObject, IShellViewModel
{
    private ILogger _logger;

    [ObservableProperty]
    private IViewModel? _current;

    public ShellViewModel(ILogger<ShellViewModel> logger)
    {
        _logger = logger;
        _logger.LogInformation("Constructor");
    }
}
