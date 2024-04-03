using IncrementalCivilization.Domain;
using IncrementalCivilization.ViewModels;
using IncrementalCivilization.ViewModels.Pages;
using IncrementalCivilization.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace IncrementalCivilization;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; private set; }
    private readonly ILogger _logger;

    public App()
    {
        Services = ConfigureServices();
        _logger = Services.GetRequiredService<ILogger<Application>>();

        InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Add logging
        services.AddLogging(builder => builder.AddDebug());

        // Add services
        services.AddSingleton<Services.INavigationService, Services.NavigationService>();

        // The application shell (a window that shows a single IViewModel)
        services.AddSingleton<ShellWindow>();
        services.AddSingleton<IShellViewModel, ShellViewModel>();

        // Application view models
        services.AddSingleton<IMainViewModel, MainViewModel>();
        services.AddSingleton<ISettingsViewModel, SettingsViewModel>();

        // Page view models
        services.AddSingleton<IPageViewModel, HomePageViewModel>();
        services.AddSingleton<IPageViewModel, ResearchPageViewModel>();
        services.AddSingleton<IPageViewModel, TimePageViewModel>();

        // Game related stuff
        services.AddSingleton<Game>();

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        _logger.LogInformation("Starting");

        var game = Services.GetRequiredService<Game>();
        game.Initialize();

        var win = Services.GetRequiredService<ShellWindow>();
        win.DataContext = Services.GetRequiredService<IShellViewModel>();
        win.Show();

        var navigation = Services.GetRequiredService<Services.INavigationService>();
        navigation.NavigateToMain();

        game.Timer.Start();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        _logger.LogInformation("Exiting");

        var game = Services.GetRequiredService<Game>();
        game.Timer.Stop();
    }
}