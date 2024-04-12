using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using NLog;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject
{
    public static readonly Logger logger = LogManager.GetCurrentClassLogger();

    private const int ticksPerSecond = 20;
    private const int ticksPerDay = 40;
    private const int daysPerYear = 4;
    private const int ticksPerPopulationIncrease = ticksPerDay * 2;
    
    public DispatcherTimer Timer { get; private set; }

    public Time Time { get; private set; }
    public Capabilities Capabilities { get; private set; }
    public Effects Effects { get; private set; }
    public ResourceBundle Resources { get; private set; }
    public BuildingsBundle Buildings { get; private set; }
    public JobsBundle Jobs { get; private set; }
    public Research Research { get; private set; }
    public Upgrades Upgrades { get; private set; }
    public List<ProgessEvent> Events { get; private set; }

    public Game()
    {
        Initialize();

        Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000 / ticksPerSecond)
        };
        Timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Time.Tick();

        Resources.Food.Add(0.125 * Buildings.Field.Count 
                         + 1.0 * Jobs.Farmer.Count * Effects.FarmerEffieciency
                         - 0.85 * Resources.Population.Value);

        Resources.Wood.Add(0.018 * Jobs.WoodCutter.Count * Effects.WoodCutterEffieciency);

        Resources.Minerals.Add(0.05 * Jobs.Miner.Count 
                             * (1 + 0.2 * Buildings.Mine.Count));

        Resources.Science.Add(0.035 * Jobs.Scholar.Count
                            * (1 + 0.1 * Buildings.Library.Count));

        if (Resources.Population.Value > 0 && Resources.Food.Value < 0)
        {
            var dead = Math.Ceiling(Resources.Food.Value / -0.85);
            Resources.Population.Sub(dead);
            StrongReferenceMessenger.Default.Send(new AddMessageLogMessage($"Food ran out {dead} died"));
        }

        if (Resources.Population.Value < Resources.Population.Maximum)
        {
            Time.PopulationTicks += 1;

            if (Time.PopulationTicks > ticksPerPopulationIncrease)
            {
                Time.PopulationTicks -= ticksPerPopulationIncrease;
                Resources.Population.Value += 1;
                StrongReferenceMessenger.Default.Send(new AddMessageLogMessage("A new person join your civilization"));
            }
        }

        Resources.Limit();
        Jobs.Limit((int)Resources.Population.Value);

        var triggeredEvents = new List<ProgessEvent>();
        foreach (var pe in Events)
        {
            if (pe.Trigger())
            {
                pe.Effect();
                triggeredEvents.Add(pe);
            }
        }

        if (triggeredEvents.Count > 0)
            foreach (var triggeredEvent in triggeredEvents)
                Events.Remove(triggeredEvent);
    }

    private void Initialize()
    {
        logger.Debug("Creating all resources etc.");
        Time = new Time(ticksPerDay, daysPerYear);
        Capabilities = new Capabilities();
        Effects = new Effects();
        Resources = new ResourceBundle();
        Buildings = new BuildingsBundle(Resources);
        Jobs = new JobsBundle();
        Research = new Research(this);
        Upgrades = new Upgrades(this);
        Events = ProgressEvents.Initialize(this);
    }

    public void Reset()
    {
        Timer.Stop();

        Initialize();
        StrongReferenceMessenger.Default.Send(new ResetMessage());

        Timer.Start();
    }
}
