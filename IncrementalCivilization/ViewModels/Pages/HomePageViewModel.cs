using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class HomePageViewModel(INavigationService navigationService, ILogger<HomePageViewModel> logger) : PageViewModelBase("Home", SymbolRegular.Home24, navigationService, logger)
{
}
