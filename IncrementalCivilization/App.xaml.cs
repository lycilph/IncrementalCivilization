﻿using IncrementalCivilization.Domain;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Pages;
using IncrementalCivilization.ViewModels.Screens;
using IncrementalCivilization.ViewModels.Shared;
using IncrementalCivilization.Views;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Windows;
using Wpf.Ui;
using INavigationService = IncrementalCivilization.Services.INavigationService;
using NavigationService = IncrementalCivilization.Services.NavigationService;

namespace IncrementalCivilization;

public partial class App : Application
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

        // Services (from WPF UI)
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<IContentDialogService, ContentDialogService>();

        // Services
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<INavigationService, NavigationService>();
        //services.AddSingleton<IMessageLog, MessageLog>();

        // Shell
        services.AddSingleton<ShellWindow>();
        services.AddSingleton<IShellViewModel, ShellViewModel>();

        // Screens
        services.AddSingleton<IMainScreenViewModel, MainScreenViewModel>();
        //services.AddSingleton<ISettingsScreenViewModel, SettingsScreenViewModel>();

        // Pages
        services.AddSingleton<IPageViewModel, HomePageViewModel>();
        services.AddSingleton<IPageViewModel, ResearchPageViewModel>();
        services.AddSingleton<IPageViewModel, UpgradesPageViewModel>();
        services.AddSingleton<IPageViewModel, TimePageViewModel>();

        // Shared
        services.AddSingleton<ResourcesViewModel>();
        services.AddSingleton<DebugViewModel>();
        //services.AddSingleton<JobsViewModel>();

        // Domain
        services.AddSingleton<Game>();

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        logger.Debug("Starting application");

        var vm = Services.GetRequiredService<IShellViewModel>();
        var shell = Services.GetRequiredService<ShellWindow>();
        
        vm.Initialize();
        shell.DataContext = vm;
        shell.Show();

        var navigation = Services.GetRequiredService<INavigationService>();
        navigation.NavigateToScreen<IMainScreenViewModel>();

        //var game = Services.GetRequiredService<Game>();
        //game.Timer.Start();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        logger.Debug("Exiting application");

        //var game = Services.GetRequiredService<Game>();
        //game.Timer.Stop();

        Settings.Default.Save();
    }
}