using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Services;
using IncrementalCivilization.Utils;
using Microsoft.Extensions.Logging;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject
{
    private const int ticksPerSecond = 20;
    private const int ticksPerDay = 40;
    private const int ticksPerPopulationIncrease = ticksPerSecond * 4;

    private readonly IMessageLog _messages;

    public ResourcesBundle Resources { get; private set; } = [];
    public BuildingsBundle Buildings { get; private set; } = [];
    public JobsBundle Jobs { get; private set; } = [];

    public DispatcherTimer Timer { get; private set; }

    [ObservableProperty]
    private int _ticks = 0;

    [ObservableProperty]
    private int _populationTicks = 0;

    [ObservableProperty]
    private int _days = 0;

    [ObservableProperty]
    private bool _isDebugging = false;

    public Game(IMessageLog messages)
    {
        _messages = messages;

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
        Jobs = JobsExtensions.AllJobs();
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

        // Resources
        var population = Resources.Population();
        var food = Resources.Food();
        var wood = Resources.Wood();
        // Buildings
        var field = Buildings.Field();
        // Jobs
        var farmer = Jobs.Farmer();
        var woodCutters = Jobs.WoodCutters();

        food.Value += 0.125 * field.Count + 1.0 * farmer.Count - 0.85 * population.Value;
        wood.Value += 0.018 * woodCutters.Count;

        if (food.Value < 0)
        {
            var dead = Math.Floor(food.Value);
            population.Value += dead;
            _messages.Add($"Your civilization ran out of food, and people starved, {dead} dead {population.Value} left");
        }

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
