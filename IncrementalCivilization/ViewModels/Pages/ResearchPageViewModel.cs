using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class ResearchPageViewModel(INavigationService navigationService) : PageViewModelBase(navigationService, "Research", SymbolRegular.Beaker24)
{
}
