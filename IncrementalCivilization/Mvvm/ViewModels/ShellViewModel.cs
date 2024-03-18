using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject? _current;
}
