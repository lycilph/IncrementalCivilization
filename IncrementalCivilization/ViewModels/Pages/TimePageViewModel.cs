using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class TimePageViewModel : PageViewModelBase
{
    public TimePageViewModel(INavigationService navigationService, Game game) : base(navigationService, "Time", SymbolRegular.HourglassHalf24)
    {
        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.ResearchPageEnabled;
    }
}
