﻿using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public class UpgradesPageViewModel : PageViewModelBase
{
    public UpgradesPageViewModel(INavigationService navigationService, Game game) : base(navigationService, "Upgrades", SymbolRegular.Star24)
    {
        game.Capabilities.PropertyChanged += (s, e) => Enabled = game.Capabilities.EnableUpgradesPage;
    }
}
