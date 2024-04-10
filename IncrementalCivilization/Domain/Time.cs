using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;

public partial class Time(int ticksPerDay, int daysPerYear) : ObservableObject
{
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
}
