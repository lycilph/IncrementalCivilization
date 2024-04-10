using IncrementalCivilization.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Views;

public partial class ShellWindow : FluentWindow
{
    public ShellWindow(IShellViewModel shellViewModel, ISnackbarService snackbarService)
    {
        InitializeComponent();

        DataContext = shellViewModel;
        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }
}