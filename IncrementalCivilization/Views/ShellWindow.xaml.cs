using Wpf.Ui;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Views;

public partial class ShellWindow : FluentWindow
{
    public ShellWindow(ISnackbarService snackbarService, IContentDialogService contentDialogService)
    {
        InitializeComponent();

        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        contentDialogService.SetContentPresenter(RootContentDialog);
    }
}