using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject
{
    private const int ticksPerSecond = 20;
    private const int ticksPerDay = 40;
    private const int ticksPerPopulationIncrease = ticksPerSecond * 4;

    public ResourcesBundle Resources { get; private set; } = [];
    public DispatcherTimer Timer { get; private set; }

    [ObservableProperty]
    private int _ticks = 0;

    [ObservableProperty]
    private int _populationTicks = 0;

    [ObservableProperty]
    private int _days = 0;

    [ObservableProperty]
    private bool _isDebugging = false;

    public Game()
    {
        Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000 / ticksPerSecond)
        };
        Timer.Tick += Timer_Tick;
    }

    public void Initialize()
    {
        Resources = ResourcesExtensions.AllResources();
    }

    public void ToogleDebugging()
    {
        IsDebugging = !IsDebugging;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Ticks += 1;

        if (Ticks >= ticksPerDay)
        {
            Ticks -= ticksPerDay;
            Days += 1;
        }

        var food = Resources.Food();
        food.Value += 0.125;
    }
}
