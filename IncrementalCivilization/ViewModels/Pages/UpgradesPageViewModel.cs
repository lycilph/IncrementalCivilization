using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class UpgradesPageViewModel(INavigationService navigationService) : PageViewModelBase(navigationService, "Upgrades", SymbolRegular.Star24)
{
}
