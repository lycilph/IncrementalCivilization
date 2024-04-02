using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class TimePageViewModel(INavigationService navigationService, ILogger<TimePageViewModel> logger) : PageViewModelBase("Time", SymbolRegular.HourglassHalf24, navigationService, logger)
{
}
