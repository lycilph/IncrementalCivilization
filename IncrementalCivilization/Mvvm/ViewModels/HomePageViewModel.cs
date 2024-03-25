using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel(IGame game) : PageViewModelBase("Home", SymbolRegular.Home24), IHomePageViewModel
{
    public ResourceBundle Resources { get => game.Resources; }
    public BuildingsBundle Buildings { get => game.Buildings; }
    public JobsBundle Jobs { get => game.Jobs; }

    [RelayCommand]
    private void CollectFood()
    {
        game.Resources[ResourceType.Food].AddAndLimit(1);
        game.Statistics.CollectFoodClicks++;
    }
}
