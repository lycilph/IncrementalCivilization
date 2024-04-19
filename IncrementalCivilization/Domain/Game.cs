using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using NLog;

namespace IncrementalCivilization.Domain;

public class Game
{
    public static readonly Logger logger = LogManager.GetCurrentClassLogger();

    private const int ticksPerSecond = 20;
    private const int ticksPerDay = 40;
    private const int daysPerYear = 4;
    private const int ticksPerPopulationIncrease = ticksPerDay * 2;

    public Time Time { get; private set; }
    public Capabilities Capabilities { get; private set; }
    public Effects Effects { get; private set; }
    public ResourceBundle Resources { get; private set; }
    public JobsBundle Jobs { get; private set; }
    public BuildingsBundle Buildings { get; private set; }
    public ResearchQueue Research {  get; private set; }
    public ProgressEventManager ProgressEventManager { get; private set; }

    public Game()
    {
        logger.Debug("Creating game object");

        Time = new Time(ticksPerSecond, ticksPerDay, daysPerYear) { TickAction = Tick };
        Capabilities = new Capabilities();
        Effects = new Effects();
        Resources = new ResourceBundle();
        Jobs = new JobsBundle();
        Buildings = new BuildingsBundle(Resources);
        Research = new ResearchQueue(this);
        ProgressEventManager = new ProgressEventManager(this);
    }

    private void Tick()
    {
        Time.Advance(); // Increments ticks, days and years
        
        // Update resources
        Resources.Food.Add(0.125 * Buildings.Field.Count
                         + 1.0 * Jobs.Farmer.Count * Effects.FarmerEffieciency
                         - 0.85 * Resources.Population.Value);

        Resources.Wood.Add(0.018 * Jobs.WoodCutter.Count * Effects.WoodCutterEffieciency);

        Resources.Minerals.Add(0.05 * Jobs.Miner.Count
                             * (1 + 0.2 * Buildings.Mine.Count));

        Resources.Science.Add(0.035 * Jobs.Scholar.Count
                            * (1 + 0.1 * Buildings.Library.Count));

        // Check if there is enough food for the population
        if (Resources.Population.Value > 0 && Resources.Food.Value < 0)
        {
            var dead = Math.Ceiling(Resources.Food.Value / -0.85);
            Resources.Population.Sub(dead);
            StrongReferenceMessenger.Default.Send(new ShowMessage($"Food ran out {dead} died"));
        }

        // Increase population
        if (Resources.Population.Value < Resources.Population.Maximum)
        {
            Time.PopulationTicks += 1;

            if (Time.PopulationTicks > ticksPerPopulationIncrease)
            {
                Time.PopulationTicks -= ticksPerPopulationIncrease;
                Resources.Population.Value += 1;
                StrongReferenceMessenger.Default.Send(new ShowMessage("A new person join your civilization"));
            }
        }

        Resources.Limit();
        Jobs.Limit((int)Resources.Population.Value);

        ProgressEventManager.Process();
    }

    public void Start() => Time.IsRunning = true;
    public void Stop() => Time.IsRunning = false;

    public void Reset()
    {
        logger.Debug("Resetting game state");

        Time.Reset();
        Capabilities.Reset();
        Effects.Reset();

        Resources.Clear();
        Resources = new ResourceBundle();

        Jobs.Clear();
        Jobs = new JobsBundle();

        Buildings.Clear();
        Buildings = new BuildingsBundle(Resources);

        Research.Clear();
        Research = new ResearchQueue(this);

        ProgressEventManager.Clear();
        ProgressEventManager = new ProgressEventManager(this);
    }
}
