using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class ResearchPageViewModel(Game game, INavigationService navigationService, ILogger<ResearchPageViewModel> logger) : PageViewModelBase("Research", SymbolRegular.Beaker24, navigationService, logger)
{
    public ObservableCollection<ResourceItem> Resources { get; private set; } = [];

    public override void Initialize()
    {
        base.Initialize();

        Resources.Add(game.Resources.Science);
    }
}
