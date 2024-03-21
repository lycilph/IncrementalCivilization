using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel(IGame game) : PageViewModelBase("Home", SymbolRegular.Home24), IHomePageViewModel
{
    public IGame Game { get => game; }

    public ObservableCollection<Resource>? Resources { get; set; }

    public ObservableCollection<Building>? Buildings { get; set; }

    public override void Initialize()
    {
        Resources = new ObservableCollection<Resource>(game.Resources.GetAll());
        Buildings = new ObservableCollection<Building>(game.Buildings.GetAll());

        base.Initialize();
    }

    [RelayCommand]
    private void CollectFood()
    {
        game.Resources[ResourceType.Food].Add(1);
    }
}
