using IncrementalCivilization.Mvvm.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.Views;

public partial class MainWindow : FluentWindow
{
    public MainWindow(IShellViewModel shellVM, ISnackbarService snackbarService)
    {
        DataContext = shellVM;

        InitializeComponent();

        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
    }
}