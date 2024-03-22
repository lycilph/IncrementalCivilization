using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel(IGame game) : PageViewModelBase("Home", SymbolRegular.Home24), IHomePageViewModel
{
    public IGame Game { get => game; }
    public ResourceBundle Resources { get => Game.Resources; }
    public BuildingsBundle Buildings { get => Game.Buildings; }

    [RelayCommand]
    private void CollectFood()
    {
        game.Resources[ResourceType.Food].Amount += 1;
        game.Statistics.CollectWoodClicks++;
    }
}
