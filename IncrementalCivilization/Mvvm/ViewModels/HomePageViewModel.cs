using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel : ObservableObject, IHomePageViewModel
{
    private readonly IGame _game;

    public string Title { get; set; }
    public SymbolRegular Icon { get; set; }

    public ObservableCollection<Resource> Resources { get; set; }

    public HomePageViewModel(IGame game)
    {
        Title = "Home";
        Icon = SymbolRegular.Home24;

        _game = game;
        Resources = new ObservableCollection<Resource>(_game.Resources.GetAll());
    }

    [RelayCommand]
    private void CollectFood()
    {
        var food = _game.Resources[ResourceType.Food];

        if (!food.Active)
            food.Active = true;

        food.Amount += 1;
    }
}
