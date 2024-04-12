using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.ViewModels.Shared;
using NLog;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class TimePageViewModel : PageViewModelBase
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    private readonly IContentDialogService _contentDialogService;

    public TimePageViewModel(Services.INavigationService navigationService, IContentDialogService contentDialogService, Game game) : base(navigationService, "Time", SymbolRegular.HourglassHalf24)
    {
        _contentDialogService = contentDialogService;

        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.TimePageEnabled;
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

        var text = result switch
        {
            ContentDialogResult.Primary => "Reset dialog - choice = Yes",
            ContentDialogResult.Secondary => "Reset dialog - choice = no",
            _ => "Reset dialog - choice = Cancel",
        };
        logger.Debug(text);


    }
}
