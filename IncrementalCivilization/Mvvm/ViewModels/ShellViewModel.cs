using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class ShellViewModel : ObservableObject, IShellViewModel
{
    [ObservableProperty]
    private IViewModel? _current;
}
