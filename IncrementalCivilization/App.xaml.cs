using IncrementalCivilization.Mvvm.Services;
using IncrementalCivilization.Mvvm.ViewModels;
using IncrementalCivilization.Mvvm.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;

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

        services.AddSingleton(provider => new MainWindow 
        {
            DataContext = provider.GetRequiredService<IShellViewModel>()
        });

        services.AddSingleton<IShellViewModel, ShellViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();
        services.AddSingleton<ISettingsViewModel, SettingsViewModel>();

        services.AddSingleton<IPageViewModel, HomePageViewModel>();
        services.AddSingleton<IPageViewModel, ResearchPageViewModel>();
        services.AddSingleton<IPageViewModel, TimePageViewModel>();

        services.AddSingleton<INavigationService, NavigationService>();

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        Trace.WriteLine("Application - starting");

        var win = Services.GetRequiredService<MainWindow>();
        win.Show();

        var navigation = Services.GetRequiredService<INavigationService>();
        navigation.Initialize();
    }
}
