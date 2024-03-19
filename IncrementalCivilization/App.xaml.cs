using IncrementalCivilization.Mvvm.ViewModels;
using IncrementalCivilization.Mvvm.Views;
using System.Windows;

namespace IncrementalCivilization;

public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var mainVm = new SettingsViewModel();
        //var mainVm = new MainViewModel();
        var shellVm = new ShellViewModel { Current = mainVm };
        var win = new MainWindow { DataContext = shellVm };
        win.Show();
    }
}
