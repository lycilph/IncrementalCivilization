using CommunityToolkit.Mvvm.ComponentModel;
using NLog;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Time : ObservableObject
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    private readonly DispatcherTimer timer;
    private readonly int ticksPerDay;
    private readonly int daysPerYear;

    [ObservableProperty]
    private int _ticks = 0;

    [ObservableProperty]
    private int _populationTicks = 0;

    [ObservableProperty]
    private int _days = 0;

    [ObservableProperty]
    private int _years = 0;

    public bool IsRunning 
    {
        get => timer.IsEnabled;
        set => SetProperty(timer.IsEnabled, value, timer, (t, v) => t.IsEnabled = v);
    }

    public Action TickAction { get; set; } = () => { };

    public Time(int ticksPerSecond, int ticksPerDay, int daysPerYear)
    {
        this.ticksPerDay = ticksPerDay;
        this.daysPerYear = daysPerYear;

        timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000 / ticksPerSecond) };
        timer.Tick += TimerTick;
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        TickAction();
    }

    public void Advance()
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
    }

    public void Reset()
    {
        logger.Debug("Resetting time");

        Ticks = 0;
        PopulationTicks = 0;
        Days = 0;
        Years = 0;
    }
}
