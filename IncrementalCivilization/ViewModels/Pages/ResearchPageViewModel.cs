using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class ResearchPageViewModel : PageViewModelBase
{
    private readonly Game _game;

    public ObservableCollection<Resource> Resources { get; private set; } = [];
    public ObservableCollection<Improvement> Unlocked { get => _game.Research.Unlocked; }
    public ObservableCollection<Improvement> Bought { get => _game.Research.Bought; }
    
    [ObservableProperty]
    private bool _hideResearchedInventions = true;

    public ResearchPageViewModel(INavigationService navigationService, Game game) : base(navigationService, "Research", SymbolRegular.Beaker24)
    {
        _game = game;

        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.ResearchPageEnabled;
        Unlocked.CollectionChanged += UnlockedCollectionChanged;
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

    public override void Initialize()
    {
        base.Initialize();

        Resources.Add(_game.Resources.Science);
    }
}
