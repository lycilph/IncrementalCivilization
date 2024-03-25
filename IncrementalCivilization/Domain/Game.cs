using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Mvvm.Services;
using System.Windows.Threading;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject, IGame
{
    private readonly DispatcherTimer timer;
    private readonly ISnackbarService snackbarService;
    private readonly ILogService logService;

    public Time Time { get; private set; } = new Time();
    public Statistics Statistics { get; private set; } = new Statistics();
    public ResourceBundle Resources { get; private set; } = ResourceBundle.AllResources();
    public JobsBundle Jobs { get; private set; } = JobsBundle.AllJobs();
    public BuildingsBundle Buildings { get; private set; } = [];

    public bool IsRunning
    { 
        get => timer.IsEnabled;
        set => SetProperty(timer.IsEnabled, value, timer, (t, v) => t.IsEnabled = v);
    }

    public Game(ISnackbarService snackbarService, ILogService logService)
    {
        this.snackbarService = snackbarService;
        this.logService = logService;

        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000 / Time.ticksPerSecond)
        };
        timer.Tick += TimerTick;
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        Time.Update();

        UpdateResources();
        UpdateBuildings();
        UpdateProgress();
    }

    private void UpdateResources()
    {
        // Clear all rates
        foreach (var item in Resources)
            item.Rate = 0;

        // Food production
        var food = Buildings[BuildingType.Field].Amount * 0.1 - Resources[ResourceType.People].Amount * 0.85;
        Resources[ResourceType.Food].Amount += food;
        if (Resources[ResourceType.Food].Amount < 0)
        {
            var dead = Math.Ceiling(Resources[ResourceType.Food].Amount / -0.85);

            Resources[ResourceType.Food].Zero();
            Resources[ResourceType.People].Amount -= dead;

            logService.Add($"People starved and {dead} died");
        }
        Resources[ResourceType.Food].Limit();

        // Update people
        if (Resources[ResourceType.People].Amount < Resources[ResourceType.People].Threshold)
        {
            Time.PopulationTicks += 1;
            if (Time.PopulationTicks >= Time.ticksPerPopulationIncrease)
            {
                Time.PopulationTicks = 0;
                Resources[ResourceType.People].Amount += 1;
            }
        }
    }

    private void UpdateBuildings()
    {
        // This updates if the player can afford the buildings
        foreach (var b in Buildings)
            b.Update(Resources);
    }

    private void UpdateProgress()
    {
        if (!Buildings[BuildingType.Field].Active && Resources[ResourceType.Food].Amount >= 5)
        {
            Buildings[BuildingType.Field].Active = true;
            ShowMessage("Progress", "With enough food, you have time to invent the field");
        }

        if (!Resources[ResourceType.Wood].Active && Statistics.CollectFoodClicks >= 10)
        {
            Resources[ResourceType.Wood].Amount += 5;
            Buildings[BuildingType.Hut].Active = true;
            ShowMessage("Progress", "While foraging for food you notice some wood");
        }
    }

    private void ShowMessage(string title, string message)
    {
        snackbarService.Show(title, message);
        logService.Add(message);
    }

    public void Initialize()
    {
        // Initialize resources
        Resources[ResourceType.People].ShowRate = false;
        Resources[ResourceType.Food].Threshold = 100;
        Resources[ResourceType.Food].Threshold = 100;
        Resources[ResourceType.Wood].Threshold = 100; 
        Resources[ResourceType.Mineral].Threshold = 100;

        // Initialize buildings
        Buildings.Add(new Building(BuildingType.Field, ResourceBundle.Single(ResourceType.Food, 0, 10), 1.12))
                 .Add(new Building(BuildingType.Hut, ResourceBundle.Single(ResourceType.Wood, 0, 5), 2.5)
                          .AddBuyAction(() => 
                          {
                              Resources[ResourceType.People].Threshold += 2;
                              Resources[ResourceType.People].Active = true;
                          }))
                 .Add(new Building(BuildingType.Mine, ResourceBundle.Single(ResourceType.Wood, 0, 10)
                                                                    .Add(ResourceType.Mineral, 0, 5)));

        IsRunning = true;
        logService.Add("Welcome to the game");
        logService.Add("Try to collect some food");
    }

    public void Cleanup()
    {
        IsRunning = false;
    }
}