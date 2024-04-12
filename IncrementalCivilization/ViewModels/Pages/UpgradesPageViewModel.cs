using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class UpgradesPageViewModel : PageViewModelBase
{
    private readonly Game _game;

    public ResourcesViewModel Resources { get; private set; }

    public ObservableCollection<Improvement> Unlocked { get => _game.Upgrades.Unlocked; }
    public ObservableCollection<Improvement> Bought { get => _game.Upgrades.Bought; }

    [ObservableProperty]
    private bool _hideBoughtUpgrades = false;

    public UpgradesPageViewModel(INavigationService navigationService, ResourcesViewModel resources, Game game) : base(navigationService, "Upgrades", SymbolRegular.Star24)
    {
        _game = game;
        Resources = resources;

        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.UpgradesPageEnabled;
        Unlocked.CollectionChanged += UnlockedCollectionChanged;
    }

    public override void Initialize()
    {
        base.Initialize();

        foreach (var item in Unlocked)
            item.PropertyChanged += ImprovementPropertyChanged;
    }

    private void UnlockedCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            foreach (var i in e.NewItems)
                if (i is Improvement improvement && improvement != null)
                    improvement.PropertyChanged += ImprovementPropertyChanged;
    }

    private void ImprovementPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is Improvement improvement && improvement.IsBought)
        {
            improvement.PropertyChanged -= ImprovementPropertyChanged;
            Unlocked.Remove(improvement);
            Bought.Add(improvement);
        }
    }
}
