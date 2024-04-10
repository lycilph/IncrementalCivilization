using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel : PageViewModelBase
{
    [ObservableProperty]
    private bool _debugMode = false;

    public HomePageViewModel(INavigationService navigationService) : base(navigationService, "Home", SymbolRegular.Home24)
    {
        Enabled = true;
        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMode();
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateDebugMode();
    }

    private void UpdateDebugMode()
    {
        DebugMode = Settings.Default.Debug;
    }

    [RelayCommand]
    private void EnableAllPages()
    {
        StrongReferenceMessenger.Default.Send(new EnablePageMessage(EnablePageMessage.Page.All));
    }
}
