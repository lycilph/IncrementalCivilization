using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Threading;

namespace IncrementalCivilization.Domain;

public partial class Game : ObservableObject, IGame
{
    private readonly DispatcherTimer timer;

    public Time Time { get; private set; } = new Time();

    public ResourceBundle Resources { get; private set; } = ResourceBundle.AllResources();

    public bool IsRunning
    { 
        get => timer.IsEnabled;
        set => SetProperty(timer.IsEnabled, value, timer, (t, v) => t.IsEnabled = v);
    }

    public Game()
    {
        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1000 / Time.ticksPerSecond)
        };
        timer.Tick += TimerTick;
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        Time.Update();

        // DEBUG
        //var wood = Resources.Get(ResourceType.Wood);
        //wood.Amount += 0.01;

        // Debug
        //if (wood.Amount > 1)
        //{
        //    Resources.Get(ResourceType.Mineral).Active = true;
        //}
    }

    public void Initialize()
    {
        IsRunning = true;
    }
}
