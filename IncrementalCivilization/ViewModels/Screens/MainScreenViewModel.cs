using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Properties;
using IncrementalCivilization.ViewModels.Shared;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class MainScreenViewModel : ViewModelBase, IMainScreenViewModel
{
    [ObservableProperty]
    private string _title = string.Empty;

    public ObservableCollection<IPageViewModel> Pages { get; private set; }

    [ObservableProperty]
    private IPageViewModel? _currentPage;

    public MainScreenViewModel(IEnumerable<IPageViewModel> pages)
    {
        Pages = new ObservableCollection<IPageViewModel>(pages);

        Settings.Default.PropertyChanged += (s, e) => Updatetitle();
    }

    public override void Initialize()
    {
        base.Initialize();
        Updatetitle();
        CurrentPage = Pages.FirstOrDefault();
    }

    private void Updatetitle()
    {
        Title = $"Incremental Civilization {Settings.Default.Debug}";
    }
}
