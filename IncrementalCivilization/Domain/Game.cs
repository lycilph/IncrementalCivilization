using CommunityToolkit.Mvvm.ComponentModel;
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
    public ResourceBundle Resources { get; private set; }
    public BuildingsBundle Buildings { get; private set; }
    public JobsBundle Jobs { get; private set; }

    public Game()
    {
        logger.Debug("Creating all resources");
        Time = new Time(ticksPerDay, daysPerYear);
        Capabilities = new Capabilities();
        Resources = new ResourceBundle();
        Buildings = new BuildingsBundle(Resources);
        Jobs = new JobsBundle();

        Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000 / ticksPerSecond)
        };
        Timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Time.Tick();

        Resources.Food.Add(0.125 * Buildings.Field.Count + 1.0 * Jobs.Farmer.Count - 0.85 * Resources.Population.Value);
        Resources.Wood.Add(0.018 * Jobs.WoodCutter.Count);
        Resources.Science.Add(0.035 * Jobs.Scholar.Count);

        if (Resources.Population.Value > 0 && Resources.Food.Value < 0)
        {
            var dead = Math.Ceiling(Resources.Food.Value / -0.85);
            Resources.Population.Sub(dead);
            logger.Debug("Food ran out {dead} died", dead);
        }

        if (Resources.Population.Value < Resources.Population.Maximum)
        {
            Time.PopulationTicks += 1;

            if (Time.PopulationTicks > ticksPerPopulationIncrease)
            {
                Time.PopulationTicks -= ticksPerPopulationIncrease;
                Resources.Population.Value += 1;
                //AddMessage("A new person joined your civilization");
            }
        }

        Resources.Limit();
        Jobs.Limit((int)Resources.Population.Value);
    }
}
