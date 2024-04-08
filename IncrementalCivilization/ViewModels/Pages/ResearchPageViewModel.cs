using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Items;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class ResearchPageViewModel : PageViewModelBase
{
    private readonly Game game;

    public ObservableCollection<ResourceItem> Resources { get; private set; } = [];
    public ObservableCollection<ResearchViewModel> Research { get; private set; } = [];

    [ObservableProperty]
    private bool _hideResearchedInventions = true;

    public ResearchPageViewModel(Game game, INavigationService navigationService, ILogger<ResearchPageViewModel> logger) : base("Research", SymbolRegular.Beaker24, navigationService, logger)
    {
        this.game = game;

        game.Effects.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(game.Effects.ResearchPageEnabled))
                Enabled = game.Effects.ResearchPageEnabled;
        };
    }

    public override void Initialize()
    {
        base.Initialize();

        Resources.Add(game.Resources.Science);
        Research = new ObservableCollection<ResearchViewModel>(game.Research.Items.Select(i => new ResearchViewModel(i)));
    }
}
