using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel(IGame game) : PageViewModelBase("Home", SymbolRegular.Home24), IHomePageViewModel
{
    private readonly IGame game = game;

    public ObservableCollection<Resource>? Resources { get; set; }

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
