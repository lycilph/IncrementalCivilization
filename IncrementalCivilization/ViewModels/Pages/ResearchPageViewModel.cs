using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class ResearchPageViewModel(INavigationService navigationService, ILogger<ResearchPageViewModel> logger) : PageViewModelBase("Research", SymbolRegular.Beaker24, navigationService, logger)
{
}
