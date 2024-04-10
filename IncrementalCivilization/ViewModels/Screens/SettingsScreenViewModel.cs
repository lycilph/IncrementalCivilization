using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using System.ComponentModel;

namespace IncrementalCivilization.ViewModels.Screens;

public partial class SettingsScreenViewModel : ViewModelBase, ISettingsScreenViewModel
{
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private bool _debugMode = false;

    public SettingsScreenViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMode();
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateDebugMode();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(DebugMode))
            Settings.Default.Debug = DebugMode;
    }

    private void UpdateDebugMode()
    {
        DebugMode = Settings.Default.Debug;
    }

    [RelayCommand]
    private void ShowMain()
    {
        navigationService.NavigateToScreen<IMainScreenViewModel>();
    }
}
