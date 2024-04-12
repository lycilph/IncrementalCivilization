using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;
public partial class Capabilities : ObservableObject
{
    [ObservableProperty]
    private bool _refineFoodEnabled = false;

    [ObservableProperty]
    private bool _researchPageEnabled = false;

    [ObservableProperty]
    private bool _upgradesPageEnabled = false;

    [ObservableProperty]
    private bool _timePageEnabled = false;
}
