using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Properties;
using IncrementalCivilization.Services;
using IncrementalCivilization.Utils;
using IncrementalCivilization.ViewModels.Shared;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Pages;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly Game game;

    public ResourcesViewModel Resources { get; private set; }
    public JobsViewModel Jobs { get; private set; }
    public IEnumerable<Building> Buildings { get => game.Buildings; }
    public Capabilities Capabilities { get => game.Capabilities; }

    [ObservableProperty]
    private bool _debugMode = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RefineFoodCommand))]
    private bool _canRefineFood = false;

    public HomePageViewModel(INavigationService navigationService, ResourcesViewModel resources, JobsViewModel jobs, Game game) : base(navigationService, "Home", SymbolRegular.Home24)
    {
        this.game = game;
        Resources = resources;
        Jobs = jobs;

        Enabled = true;
        Settings.Default.PropertyChanged += (s, e) => UpdateDebugMode();
        game.Resources.Food.PropertyChanged += (s, e) => CanRefineFood = game.Resources.Food.Value > 100;
    }

    public override void Initialize()
    {
        base.Initialize();
        UpdateDebugMode();
    }

    private void UpdateDebugMode()
    {
        DebugMode = Settings.Default.Debug;
    }

    [RelayCommand]
    private void GatherFood()
    {
        game.Resources.Food.Add(1, skipRateUpdate: true);
    }

    [RelayCommand(CanExecute = nameof(CanRefineFood))]
    private void RefineFood()
    {
        game.Resources.Wood.Add(1, skipRateUpdate: true);
        game.Resources.Food.Sub(100, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddPopulation()
    {
        game.Resources.Population.Add(1, skipRateUpdate: true);
    }

    [RelayCommand]
    private void RemovePopulation()
    {
        game.Resources.Population.Sub(1, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddPopulationMax()
    {
        game.Resources.Population.Maximum += 1;
    }

    [RelayCommand]
    private void AddFood(string food)
    {
        if (int.TryParse(food, out int value))
            game.Resources.Food.Add(value, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddWood(string wood)
    {
        if (int.TryParse(wood, out int value))
            game.Resources.Wood.Add(value, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddScience(string science)
    {
        if (int.TryParse(science, out int value))
            game.Resources.Science.Add(value, skipRateUpdate: true);
    }

    [RelayCommand]
    private void ClearResources()
    {
        game.Resources.Apply(r => r.Value = 0);
    }

    [RelayCommand]
    private void AddField(string amount)
    {
        if (int.TryParse(amount, out int value))
            for (int i = 0; i < value; i++)
                game.Buildings.Field.Buy();
    }

    [RelayCommand]
    private void AddHut(string amount)
    {
        if (int.TryParse(amount, out int value))
            for (int i = 0; i < value; i++)
                game.Buildings.Hut.Buy();
    }

    [RelayCommand]
    private void AddLibrary(string amount)
    {
        if (int.TryParse(amount, out int value))
            for (int i = 0; i < value; i++)
                game.Buildings.Library.Buy();
    }

    [RelayCommand]
    private void ClearBuildings()
    {
        game.Buildings.Apply(b => b.Count = 0);
    }

    [RelayCommand]
    private void EnableAllPages()
    {
        game.Capabilities.ResearchPageEnabled = true;
        game.Capabilities.UpgradesPageEnabled = true;
        game.Capabilities.TimePageEnabled = true;
    }

    [RelayCommand]
    private void EnableRefineFood()
    {
        game.Capabilities.RefineFoodEnabled = true;
    }

    [RelayCommand]
    private void EnableAllResources()
    {
        game.Resources.Apply(r => r.Active = true);
    }

    [RelayCommand]
    private void EnableAllBuildings()
    {
        game.Buildings.Apply(b => b.Active = true);
    }

    [RelayCommand]
    private void EnableAllJobs()
    {
        game.Jobs.Apply(j => j.Active = true);
    }
}
