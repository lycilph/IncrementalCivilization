using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;

public partial class Time : ObservableObject
{
    public const int ticksPerSecond = 20;
    public const int ticksPerDay = 40;

    [ObservableProperty]
    private int _ticks = 0;

    [ObservableProperty]
    private int _day = 0;

    public void Update()
    {
        Ticks += 1;

        if (Ticks > ticksPerDay)
        {
            Day += 1;
            Ticks -= ticksPerDay;
        }
    }
}
