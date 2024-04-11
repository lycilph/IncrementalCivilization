using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;
public partial class Capabilities : ObservableObject
{
    [ObservableProperty]
    private bool _refineFoodEnabled = false;

    [ObservableProperty]
    private bool _enableResearchPage = false;

    [ObservableProperty]
    private bool _enableUpgradesPage = false;

    [ObservableProperty]
    private bool _enableTimePage = false;
}
