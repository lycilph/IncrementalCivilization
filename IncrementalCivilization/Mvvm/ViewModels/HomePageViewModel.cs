using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel : PageViewModelBase, IHomePageViewModel
{
    private readonly IGame game;

    public ObservableCollection<Resource>? Resources { get; set; }

    public HomePageViewModel(IGame game) : base("Home", SymbolRegular.Home24)
    {
        this.game = game;
    }

    public override void Initialize()
    {
        Resources = new ObservableCollection<Resource>(game.Resources.GetAll());
        base.Initialize();
    }

    [RelayCommand]
    private void CollectFood()
    {
        game.Resources[ResourceType.Food].Add(1);
    }
}
