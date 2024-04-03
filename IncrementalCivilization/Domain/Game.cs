using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject
{
    private const int ticksPerSecond = 20;
    private const int ticksPerDay = 40;
    private const int ticksPerPopulationIncrease = ticksPerSecond * 4;

    public ResourcesBundle Resources { get; private set; } = [];
    public BuildingsBundle Buildings { get; private set; } = [];
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
        Buildings = BuildingsExtensions.AllBuildings(Resources);
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
        var population = Resources.Population();
        var field = Buildings.Field();

        food.Value += 0.125 * field.Count - 0.85 * population.Value;

        if (population.Value < population.Maximum)
        {
            PopulationTicks += 1;

            if (PopulationTicks > ticksPerPopulationIncrease)
            {
                PopulationTicks -= ticksPerPopulationIncrease;
                population.Value += 1;
            }
        }

        Resources.Apply(i => i.Limit());
    }
}
