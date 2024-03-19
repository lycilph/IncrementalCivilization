using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class HomePageViewModel : ObservableObject, IHomePageViewModel
{
    public string Title { get; set; }
    public SymbolRegular Icon { get; set; }

    public HomePageViewModel()
    {
        Title = "Home";
        Icon = SymbolRegular.Home24;
    }
}
