using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Services;
using IncrementalCivilization.Utils;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject
{
    private const int ticksPerSecond = 20;
    private const int ticksPerDay = 40;
    private const int daysPerYear = 100;
    private const int ticksPerPopulationIncrease = ticksPerSecond * 4;

    private readonly IMessageLog _messages;

    public ResourcesBundle Resources { get; private set; } = [];
    public BuildingsBundle Buildings { get; private set; } = [];
    public JobsBundle Jobs { get; private set; } = [];
    public ResearchBundle Research { get; private set; } = new();
    public UpgradesBundle Upgrades { get; private set; } = new();
    public Effects Effects { get; private set; } = new();
    public List<ProgessEvent> Events { get; private set; } = new();

    public DispatcherTimer Timer { get; private set; }

    [ObservableProperty]
    private int _ticks = 0;

    [ObservableProperty]
    private int _populationTicks = 0;

    [ObservableProperty]
    private int _days = 0;

    [ObservableProperty]
    private int _years = 0;

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
        Resources = ResourcesBundle.AllResources();
        Buildings = BuildingsBundle.AllBuildings(Resources);
        Jobs = JobsBundle.AllJobs();
        Research = ResearchBundle.Initialize(this);
        Upgrades = UpgradesBundle.Initialize(this);
        Events = ProgressEvents.Initialize(this);
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

            if (Days >= daysPerYear) 
            {
                Days -= daysPerYear;
                Years += 1;
            }
        }

        Resources.Food.Value += 0.125 * Buildings.Field.Count + 1.0 * Jobs.Farmer.Count * Effects.FarmerEffieciency - 0.85 * Resources.Population.Value;
        Resources.Wood.Value += 0.018 * Jobs.WoodCutter.Count * Effects.WoodCutterEffieciency;
        Resources.Science.Value += 0.035 * Jobs.Scholar.Count * Effects.ScholarEffieciency * (1 + 0.1 * Buildings.Library.Count);

        if (Resources.Population.Value > 0 && Resources.Food.Value < 0)
        {
            var dead = Math.Floor(Resources.Food.Value);
            Resources.Population.Value += dead;
            Jobs.Limit(Resources.Population);
            AddMessage($"Your civilization ran out of food, and people starved, {dead} dead {Resources.Population.Value} left");
        }

        if (Resources.Population.Value < Resources.Population.Maximum)
        {
            PopulationTicks += 1;

            if (PopulationTicks > ticksPerPopulationIncrease)
            {
                PopulationTicks -= ticksPerPopulationIncrease;
                Resources.Population.Value += 1;
                AddMessage("A new person joined your civilization");
            }
        }

        Resources.Apply(i => i.Limit());

        List<ProgessEvent> triggeredEvents = [];
        foreach (var pe in Events)
        {
            if (pe.Trigger())
            {
                pe.Effect();
                triggeredEvents.Add(pe);
            }
        }

        if (triggeredEvents.Count > 0 )
            foreach (var triggeredEvent in triggeredEvents)
                Events.Remove(triggeredEvent);
    }

    public void AddMessage(string msg) => _messages.Add(msg);
}
