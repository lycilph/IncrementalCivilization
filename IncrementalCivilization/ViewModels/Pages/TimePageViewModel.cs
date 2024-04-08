using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class TimePageViewModel : PageViewModelBase
{
    public TimePageViewModel(Game game, INavigationService navigationService, ILogger<TimePageViewModel> logger) : base("Time", SymbolRegular.HourglassHalf24, navigationService, logger)
    {
        game.Effects.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(game.Effects.TimePageEnabled))
                Enabled = game.Effects.TimePageEnabled;
        };
    }
}
