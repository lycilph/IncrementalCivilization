using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Services;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Core;

public partial class PageViewModelBase(INavigationService navigationService, string title, SymbolRegular icon) : ViewModelBase, IPageViewModel
{
    public string Title { get; protected set; } = title;
    public SymbolRegular Icon { get; protected set; } = icon;

    [ObservableProperty]
    private bool _active = false;

    public override void Activate()
    {
        base.Activate();
        Active = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Active = false;
    }

    partial void OnActiveChanged(bool value)
    {
        CanNavigateToPage = !Active;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(NavigateToPageCommand))]
    private bool _canNavigateToPage = true;

    [RelayCommand(CanExecute = nameof(CanNavigateToPage))]
    protected void NavigateToPage()
    {
        navigationService.NavigateToPage(this);
    }
}
