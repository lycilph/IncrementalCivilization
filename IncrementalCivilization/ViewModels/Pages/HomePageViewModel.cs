using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel(INavigationService navigationService, ResourcesViewModel resources, DebugViewModel debugViewModel) 
    : PageViewModelBase(navigationService, "Home", SymbolRegular.Home24), IHomePageViewModel
{
    public ResourcesViewModel ResourcesVM { get => resources; }
    public DebugViewModel DebugVM { get => debugViewModel; }
    
    [ObservableProperty]
    private bool _debugMode = false;

    public override void Initialize()
    {
        base.Initialize();

        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMode();
        UpdateDebugMode();

        Enabled = true;
    }

    private void UpdateDebugMode()
    {
        DebugMode = Settings.Default.DebugMode;
    }
}
