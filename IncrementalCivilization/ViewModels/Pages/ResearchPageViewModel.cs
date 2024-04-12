using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class ResearchPageViewModel : PageViewModelBase
{
    public ResearchPageViewModel(INavigationService navigationService, Game game) : base(navigationService, "Research", SymbolRegular.Beaker24)
    {
        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.ResearchPageEnabled;
    }
}
