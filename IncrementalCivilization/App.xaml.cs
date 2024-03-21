using IncrementalCivilization.Domain;
using IncrementalCivilization.Mvvm.Services;
using IncrementalCivilization.Mvvm.ViewModels;
using IncrementalCivilization.Mvvm.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;
using Wpf.Ui;
using INavigationService = IncrementalCivilization.Mvvm.Services.INavigationService;
using NavigationService = IncrementalCivilization.Mvvm.Services.NavigationService;

namespace IncrementalCivilization;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();

        InitializeComponent();
    }

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; private set; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // The application window and shell (shows a single IViewModel)
        services.AddSingleton<MainWindow>();
        services.AddSingleton<IShellViewModel, ShellViewModel>();

        // Main views (takes up the entire window)
        services.AddSingleton<IMainViewModel, MainViewModel>();
        services.AddSingleton<ISettingsViewModel, SettingsViewModel>();

        // Pages in the main view (takes up only part of the main view)
        services.AddSingleton<IPageViewModel, HomePageViewModel>();
        services.AddSingleton<IPageViewModel, ResearchPageViewModel>();
        services.AddSingleton<IPageViewModel, TimePageViewModel>();

        // Services
        services.AddSingleton<ILogService, LogService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();

        // Game related
        services.AddSingleton<IGame, Game>();

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        Trace.WriteLine("Application - starting");

        var win = Services.GetRequiredService<MainWindow>();
        win.Show();

        var navigation = Services.GetRequiredService<INavigationService>();
        navigation.Initialize();

        var game = Services.GetRequiredService<IGame>();
        game.Initialize();
    }
}
