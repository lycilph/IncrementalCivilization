using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class PageViewModelBase(string title, SymbolRegular icon, INavigationService navigationService, ILogger logger) : ViewModelBase(logger), IPageViewModel
{
    public string Title { get; protected set; } = title;
    public SymbolRegular Icon { get; protected set; } = icon;

    [RelayCommand]
    private void NavigateToPage()
    {
        navigationService.NavigateToPage(this);
    }
}
