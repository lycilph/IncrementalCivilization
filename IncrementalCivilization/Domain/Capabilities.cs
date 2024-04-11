using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;
public partial class Capabilities : ObservableObject
{
    [ObservableProperty]
    private bool _refineFoodEnabled = false;
}
