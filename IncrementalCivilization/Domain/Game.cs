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
    public BuildingsBundle Buildings { get; private set; } = BuildingsBundle.AllBuildings();

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
        Resources[ResourceType.Food].Amount += Buildings[BuildingType.Field].Amount * 0.1;
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
            snackbarService.Show("Progress", "With enough food, you have time to invent the field");
        }

        if (!Resources[ResourceType.Wood].Active && Statistics.CollectWoodClicks >= 10)
        {
            Resources[ResourceType.Wood].Amount += 10;
            snackbarService.Show("Progress", "While foraging for food you notice some wood");
        }
    }

    public void Initialize()
    {
        IsRunning = true;
        logService.Add("Welcome to the game");
        logService.Add("Try to collect some food");
    }

    public void Cleanup()
    {
        IsRunning = false;
    }
}
