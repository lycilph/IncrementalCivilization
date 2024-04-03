﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel(Game game, INavigationService navigationService, ILogger<HomePageViewModel> logger) : PageViewModelBase("Home", SymbolRegular.Home24, navigationService, logger)
{
    public Game Game { get; set; } = game;

    [ObservableProperty]
    private bool _showRefineFood = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RefineFoodCommand))]
    private bool _canRefineFood = false;

    public override void Initialize()
    {
        base.Initialize();

        Game.Resources.Food().PropertyChanging += (s, e) =>
        {
            var item = (s as ResourceItem)!;

            CanRefineFood = item.Value >= 100;

            if (!ShowRefineFood && item.Value >= 100)
                ShowRefineFood = true;
        };
    }

    [RelayCommand]
    private void GatherFood()
    {
        Game.Resources.Food().Value += 1;
    }

    [RelayCommand(CanExecute = nameof(CanRefineFood))]
    private void RefineFood()
    {
        Game.Resources.Wood().Value += 1;
        Game.Resources.Food().Value -= 100;
    }

    [RelayCommand]
    private void AddFood()
    {
        Game.Resources.Food().Value += 100;
    }
}
