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
    public ResourceBundle Resources { get; private set; }
    public ProgressEventManager ProgressEventManager { get; private set; }

    public Game()
    {
        logger.Debug("Creating game object");

        Time = new Time(ticksPerSecond, ticksPerDay, daysPerYear) { TickAction = Tick };
        Capabilities = new Capabilities();
        Resources = new ResourceBundle();
        ProgressEventManager = new ProgressEventManager();
    }

    private void Tick()
    {
        Time.Advance(); // Increments ticks, days and years

        // Do stuff here

        Resources.Limit();

        ProgressEventManager.Process();
    }

    public void Start() => Time.IsRunning = true;
    public void Stop() => Time.IsRunning = false;

    public void Reset()
    {
        logger.Debug("Resetting game state");

        Time.Reset();
        Capabilities.Reset();

        Resources.Clear();
        Resources = new ResourceBundle();
    }
}
