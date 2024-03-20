using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Threading;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject, IGame
{
    private readonly DispatcherTimer timer;
    private readonly ISnackbarService snackbarService;

    public Time Time { get; private set; } = new Time();

    public ResourceBundle Resources { get; private set; } = ResourceBundle.AllResources();

    public bool IsRunning
    { 
        get => timer.IsEnabled;
        set => SetProperty(timer.IsEnabled, value, timer, (t, v) => t.IsEnabled = v);
    }

    public Game(ISnackbarService snackbarService)
    {
        this.snackbarService = snackbarService;
        
        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000 / Time.ticksPerSecond)
        };
        timer.Tick += TimerTick;
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        Time.Ticks += 1;

        if (Time.Ticks > Time.ticksPerDay)
        {
            Time.Day += 1;
            Time.Ticks -= Time.ticksPerDay;

            // Trigger progress
            var food = Resources[ResourceType.Food];
            var wood = Resources[ResourceType.Wood];

            if (!wood.Active && food.Amount >= 5)
            {
                wood.Active = true;
                wood.Amount += 1;
                snackbarService.Show("Wood", "While searching for food you found some wood", ControlAppearance.Light);
            }
        }
    }

    public void Initialize()
    {
        IsRunning = true;
    }
}
