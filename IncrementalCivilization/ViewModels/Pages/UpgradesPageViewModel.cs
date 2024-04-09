using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Items;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class UpgradesPageViewModel : PageViewModelBase
{
    private readonly Game game;

    public IEnumerable<ResourceItem> Resources { get => game.Resources; }
    public ObservableCollection<ImprovementViewModel> Upgrades { get; private set; } = [];

    [ObservableProperty]
    private bool _hideResearchedInventions = true;

    public UpgradesPageViewModel(Game game, INavigationService navigationService, ILogger<ResearchPageViewModel> logger) : base("Upgrades", SymbolRegular.Star24, navigationService, logger)
    {
        this.game = game;

        game.Effects.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(game.Effects.UpgradesPageEnabled))
                Enabled = game.Effects.UpgradesPageEnabled;
        };
    }

    public override void Initialize()
    {
        base.Initialize();

        Upgrades = new ObservableCollection<ImprovementViewModel>(game.Upgrades.Items.Select(i => new ImprovementViewModel(i)));

        game.Upgrades.Items.CollectionChanged += Items_CollectionChanged;
    }

    private void Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add || e.NewItems == null)
            throw new NotSupportedException("Only additions are supported");

        foreach (var m in e.NewItems.Cast<ImprovementItem>())
            Upgrades.Add(new ImprovementViewModel(m));
    }
}
