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
}
