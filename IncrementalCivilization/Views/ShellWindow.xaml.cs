using IncrementalCivilization.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Views;

public partial class ShellWindow : FluentWindow
{
    public ShellWindow(IShellViewModel shellViewModel, ISnackbarService snackbarService, IContentDialogService contentDialogService)
    {
        InitializeComponent();

        DataContext = shellViewModel;

        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        contentDialogService.SetContentPresenter(RootContentDialog);
    }
}