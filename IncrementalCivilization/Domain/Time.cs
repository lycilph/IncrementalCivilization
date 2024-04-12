using CommunityToolkit.Mvvm.ComponentModel;
using NLog;

namespace IncrementalCivilization.Domain;

public partial class Time(int ticksPerDay, int daysPerYear) : ObservableObject
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    [ObservableProperty]
    private int _ticks = 0;

    [ObservableProperty]
    private int _populationTicks = 0;

    [ObservableProperty]
    private int _days = 0;

    [ObservableProperty]
    private int _years = 0;

    public void Tick()
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
