using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.ViewModels.Core;
using NLog;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;
using INavigationService = IncrementalCivilization.Services.INavigationService;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class TimePageViewModel : PageViewModelBase
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    
    private readonly INavigationService _navigationService;
    private readonly IContentDialogService _contentDialogService;
    private readonly Game _game;

    public TimePageViewModel(INavigationService navigationService, IContentDialogService contentDialogService, Game game) : base(navigationService, "Time", SymbolRegular.HourglassHalf24)
    {
        _navigationService = navigationService;
        _contentDialogService = contentDialogService;
        _game = game;

        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.ResearchPageEnabled;
    }

    [RelayCommand]
    private async Task Reset()
    {
        var result = await _contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
        {
            Title = "Warning!",
            Content = "Are you sure you want to reset the game?",
            PrimaryButtonText = "Yes",
            SecondaryButtonText = "No",
            CloseButtonText = "Cancel"
        });
                
        if (result == ContentDialogResult.Primary)
        {
            logger.Debug("Resetting game...");
            _game.Reset();
            _navigationService.NavigateToPage<IHomePageViewModel>();
        }
    }
}
