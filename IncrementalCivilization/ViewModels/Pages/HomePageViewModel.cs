﻿using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel(INavigationService navigationService, ISettingsService settingsService, ResourcesViewModel resources, DebugViewModel debugViewModel) 
    : PageViewModelBase(navigationService, "Home", SymbolRegular.Home24)
{
    public ResourcesViewModel ResourcesVM { get => resources; }
    public DebugViewModel DebugVM { get => debugViewModel; }
    
    [ObservableProperty]
    private bool _debugMode = false;

    public override void Initialize()
    {
        base.Initialize();

        settingsService.PropertyChanged += (s, e) => UpdateDebugMode();
        UpdateDebugMode();

        Enabled = true;
    }

    private void UpdateDebugMode()
    {
        DebugMode = settingsService.Debug;
    }
}
