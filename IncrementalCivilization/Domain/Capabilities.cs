using CommunityToolkit.Mvvm.ComponentModel;
using NLog;

namespace IncrementalCivilization.Domain;
public partial class Capabilities : ObservableObject
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    [ObservableProperty]
    private bool _refineFoodEnabled = false;

    [ObservableProperty]
    private bool _researchPageEnabled = false;

    [ObservableProperty]
    private bool _upgradesPageEnabled = false;

    [ObservableProperty]
    private bool _timePageEnabled = false;

    public void Reset()
    {
        logger.Debug("Resetting capabilities");

        RefineFoodEnabled = false;
        ResearchPageEnabled = false;
        UpgradesPageEnabled = false;
        TimePageEnabled = false;
    }
}
