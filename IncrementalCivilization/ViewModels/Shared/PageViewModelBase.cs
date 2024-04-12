using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Services;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class PageViewModelBase(INavigationService navigationService, string title, SymbolRegular icon) : ViewModelBase, IPageViewModel
{
    public string Title { get; protected set; } = title;
    public SymbolRegular Icon { get; protected set; } = icon;

    [RelayCommand]
    protected void NavigateToPage()
    {
        navigationService.NavigateToPage(this);
    }
}
