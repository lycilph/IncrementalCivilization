using IncrementalCivilization.ViewModels;
using IncrementalCivilization.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;
using Wpf.Ui;

namespace IncrementalCivilization;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; private set; }
    private ILogger _logger;

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

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        _logger.LogInformation("Starting");

        var win = Services.GetRequiredService<ShellWindow>();
        win.DataContext = Services.GetRequiredService<IShellViewModel>();
        win.Show();

        var navigation = Services.GetRequiredService<Services.INavigationService>();
        navigation.NavigateTo<IMainViewModel>();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        _logger.LogInformation("Exiting");
    }
}