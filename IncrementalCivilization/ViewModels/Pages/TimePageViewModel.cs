using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class TimePageViewModel(INavigationService navigationService) : PageViewModelBase(navigationService, "Time", SymbolRegular.HourglassHalf24)
{
}
