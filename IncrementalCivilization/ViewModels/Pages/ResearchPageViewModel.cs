using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class ResearchPageViewModel : PageViewModelBase
{
    private readonly Game _game;
 
    public ObservableCollection<Resource> Resources { get; private set; } = [];
    public ObservableCollection<Improvement> Unlocked { get => _game.Research.Unlocked; }
    public ObservableCollection<Improvement> Bought { get => _game.Research.Bought; }

    [ObservableProperty]
    private bool _hideResearchedInventions = false;

    public ResearchPageViewModel(INavigationService navigationService, Game game) : base(navigationService, "Research", SymbolRegular.Beaker24)
    {
        _game = game;
        _game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.ResearchPageEnabled;
    }

    public override void Activate()
    {
        base.Activate();

        Resources.Add(_game.Resources.Science);
    }

    public override void Deactivate()
    {
        base.Deactivate();

        Resources.Clear();
    }
}
