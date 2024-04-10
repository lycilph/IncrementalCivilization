using IncrementalCivilization.Properties;
using IncrementalCivilization.ViewModels;
using IncrementalCivilization.ViewModels.Pages;
using IncrementalCivilization.ViewModels.Screens;
using IncrementalCivilization.ViewModels.Shared;
using IncrementalCivilization.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Wpf.Ui;
using INavigationService = IncrementalCivilization.Services.INavigationService;
using NavigationService = IncrementalCivilization.Services.NavigationService;

namespace IncrementalCivilization;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; private set; }

    public App()
    {
        Services = ConfigureServices();

        InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Shell
        services.AddSingleton<ShellWindow>();
        services.AddSingleton<IShellViewModel, ShellViewModel>();

        // Screens
        services.AddSingleton<IMainScreenViewModel, MainScreenViewModel>();

        // Pages
        services.AddSingleton<IPageViewModel, HomePageViewModel>();
        services.AddSingleton<IPageViewModel, ResearchPageViewModel>();
        services.AddSingleton<IPageViewModel, UpgradesPageViewModel>();
        services.AddSingleton<IPageViewModel, TimePageViewModel>();

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var shell = Services.GetRequiredService<ShellWindow>();
        shell.Show();

        var navigation = Services.GetRequiredService<INavigationService>();
        navigation.NavigateToScreen<IMainScreenViewModel>();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        Settings.Default.Save();
    }
}