using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace IncrementalCivilization.ViewModels;

public partial class ShellViewModel(ILogger<ShellViewModel> logger) : ViewModelBase(logger), IShellViewModel
{
    [ObservableProperty]
    private IViewModel? _current;
}
