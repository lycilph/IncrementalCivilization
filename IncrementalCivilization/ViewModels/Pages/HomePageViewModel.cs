using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Services;
using IncrementalCivilization.Utils;
using IncrementalCivilization.ViewModels.Items;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel(Game game, ISettingsService settingsService, INavigationService navigationService, ILogger<HomePageViewModel> logger) : PageViewModelBase("Home", SymbolRegular.Home24, navigationService, logger)
{
    public Game Game { get; private set; } = game;

    public ObservableCollection<BuildingViewModel> Buildings { get; private set; } = [];
    public ObservableCollection<JobViewModel> Jobs { get; private set; } = [];
    public ResourceItem FreePopulation { get; private set; } = new ResourceItem(ResourceItemType.Population);

    [ObservableProperty]
    private bool _showJobs = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RefineFoodCommand))]
    private bool _canRefineFood = false;

    public override void Initialize()
    {
        base.Initialize();

        Enabled = true;

        Buildings = new ObservableCollection<BuildingViewModel>(Game.Buildings.Select(b => new BuildingViewModel(b)));
        Jobs = new ObservableCollection<JobViewModel>(Game.Jobs.Select(j => new JobViewModel(j, FreePopulation)));

        Game.Resources.Food.PropertyChanged += (s, e) => CanRefineFood = Game.Resources.Food.Value >= 100;

        Game.Resources.Population.PropertyChanged += PopulationPropertyChanged;
        foreach (var job in Game.Jobs)
        {
            job.PropertyChanged += PopulationPropertyChanged;
            job.PropertyChanged += JobPropertyChanged;
        }
    }

    private void JobPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ShowJobs = Game.Jobs.Any(j => j.Active);
    }

    private void PopulationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        FreePopulation.Maximum = Game.Resources.Population.Value;
        FreePopulation.Value = FreePopulation.Maximum - Jobs.Aggregate(0, (a, b) => a + b.Item.Count);
    }

    [RelayCommand]
    private void GatherFood()
    {
        Game.Resources.Food.Add(1, skipRateUpdate: true);
    }

    [RelayCommand(CanExecute = nameof(CanRefineFood))]
    private void RefineFood()
    {
        Game.Resources.Wood.Add(1, true);
        Game.Resources.Food.Subtract(100, skipRateUpdate: true);
    }

    [RelayCommand]
    private void ClearJobs()
    {
        Game.Jobs.Apply(j => j.Count = 0);
    }

    [RelayCommand]
    private void AddFood()
    {
        var option = settingsService.Get(OptionType.DebugFood);
        if (int.TryParse(option.Value, out int value))
        {
            Game.Resources.Food.Add(value, skipRateUpdate: true);
            _logger.LogInformation("Adding {food} to food", value);
        }
        else
            _logger.LogInformation("Couldn't parse option {name}", option.Name);
    }

    [RelayCommand]
    private void AddWood()
    {
        var option = settingsService.Get(OptionType.DebugWood);
        if (int.TryParse(option.Value, out int value))
        {
            Game.Resources.Wood.Add(value, skipRateUpdate: true);
            _logger.LogInformation("Adding {wood} to wood", value);
        }
        else
            _logger.LogInformation("Couldn't parse option {name}", option.Name);
    }

    [RelayCommand]
    private void AddScience()
    {
        var option = settingsService.Get(OptionType.DebugScience);
        if (int.TryParse(option.Value, out int value))
        {
            Game.Resources.Science.Add(value, skipRateUpdate: true);
            _logger.LogInformation("Adding {science} to science", value);
        }
        else
            _logger.LogInformation("Couldn't parse option {name}", option.Name);
    }

    [RelayCommand]
    private void ClearResources()
    {
        Game.Resources.Food.Value = 0;
        Game.Resources.Wood.Value = 0;
        Game.Resources.Science.Value = 0;
    }

    [RelayCommand]
    private void AddField()
    {
        Game.Buildings.Field.Buy();
    }

    [RelayCommand]
    private void AddHut()
    {
        Game.Buildings.Hut.Buy();
    }
}
