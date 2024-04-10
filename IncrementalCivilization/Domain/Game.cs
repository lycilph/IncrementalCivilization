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
    public ResourceBundle Resources { get; private set; }

    public Game()
    {
        logger.Debug("Creating all resources");
        Time = new Time(ticksPerDay, daysPerYear);
        Resources = new ResourceBundle();

        Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000 / ticksPerSecond)
        };
        Timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Time.Tick();
    }
}
