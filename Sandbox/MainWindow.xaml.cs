using Sandbox.Domain;
using Sandbox.Utils;
using Sandbox.ViewModels;
using System.Windows;
using System.Windows.Threading;

namespace Sandbox;

public partial class MainWindow : Window
{
    private DispatcherTimer timer;

    public int Ticks
    {
        get { return (int)GetValue(TicksProperty); }
        set { SetValue(TicksProperty, value); }
    }
    public static readonly DependencyProperty TicksProperty =
        DependencyProperty.Register("Ticks", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

    public int PopulationTicks
    {
        get { return (int)GetValue(PopulationTicksProperty); }
        set { SetValue(PopulationTicksProperty, value); }
    }
    public static readonly DependencyProperty PopulationTicksProperty =
        DependencyProperty.Register("PopulationTicks", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

    public ResourcesBundle GameResources
    {
        get { return (ResourcesBundle)GetValue(GameResourcesProperty); }
        set { SetValue(GameResourcesProperty, value); }
    }
    public static readonly DependencyProperty GameResourcesProperty =
        DependencyProperty.Register("GameResources", typeof(ResourcesBundle), typeof(MainWindow), new PropertyMetadata(null));

    public BuildingsBundle GameBuildings
    {
        get { return (BuildingsBundle)GetValue(GameBuildingsProperty); }
        set { SetValue(GameBuildingsProperty, value); }
    }
    public static readonly DependencyProperty GameBuildingsProperty =
        DependencyProperty.Register("GameBuildings", typeof(BuildingsBundle), typeof(MainWindow), new PropertyMetadata(null));

    public JobsBundle GameJobs
    {
        get { return (JobsBundle)GetValue(GameJobsProperty); }
        set { SetValue(GameJobsProperty, value); }
    }
    public static readonly DependencyProperty GameJobsProperty =
        DependencyProperty.Register("GameJobs", typeof(JobsBundle), typeof(MainWindow), new PropertyMetadata(null));

    public BuildingsBundleViewModel BuildingsVM
    {
        get { return (BuildingsBundleViewModel)GetValue(BuildingsVMProperty); }
        set { SetValue(BuildingsVMProperty, value); }
    }
    public static readonly DependencyProperty BuildingsVMProperty =
        DependencyProperty.Register("BuildingsVM", typeof(BuildingsBundleViewModel), typeof(MainWindow), new PropertyMetadata(null));

    public JobsBundleViewModel JobsVM
    {
        get { return (JobsBundleViewModel)GetValue(JobsVMProperty); }
        set { SetValue(JobsVMProperty, value); }
    }
    public static readonly DependencyProperty JobsVMProperty =
        DependencyProperty.Register("JobsVM", typeof(JobsBundleViewModel), typeof(MainWindow), new PropertyMetadata(null));

    public bool CanRefineFood
    {
        get { return (bool)GetValue(CanRefineFoodProperty); }
        set { SetValue(CanRefineFoodProperty, value); }
    }
    public static readonly DependencyProperty CanRefineFoodProperty =
        DependencyProperty.Register("CanRefineFood", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000 / 20)
        };
        timer.Tick += Timer_Tick;

        GameResources = ResourceExtensions.AllResources();
        GameBuildings = BuildingsExtensions.AllBuildings(GameResources);
        GameJobs = JobsExtensions.AllJobs();

        BuildingsVM = new BuildingsBundleViewModel(GameBuildings);
        JobsVM = new JobsBundleViewModel(GameJobs, GameResources.Population());

        GameResources.Food().PropertyChanging += (s, e) => 
        {
            if (s is ResourceItem i)
            {
                CanRefineFood = i.Value >= 100;
            }
        };

        timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Ticks += 1;

        GameResources.Food().Value += 0.125 * GameBuildings.Field().Count + GameJobs.Farmer().Count - 0.85 * GameResources.Population().Value;

        GameResources.Wood().Value += 0.018 * GameJobs.WoodCutters().Count;

        var pop = GameResources.Population();
        if (pop.Value < pop.Maximum)
        {
            PopulationTicks += 1;

            if (PopulationTicks >= 40)
            {
                PopulationTicks -= 40;
                pop.Value += 1;
            }
        }

        GameResources.Apply(i => i.Limit());
    }

    private void AddFoodClick(object sender, RoutedEventArgs e)
    {
        GameResources.Food().Value += 10;
    }

    private void AddWoodClick(object sender, RoutedEventArgs e)
    {
        GameResources.Wood().Value += 10;
    }

    private void AddPeopleClick(object sender, RoutedEventArgs e)
    {
        GameResources.Population().Value += 1;
    }

    private void LimitAllClick(object sender, RoutedEventArgs e)
    {
        GameResources.Apply(i => i.Limit());
    }

    private void StartClick(object sender, RoutedEventArgs e)
    {
        timer.Start();
    }

    private void StopClick(object sender, RoutedEventArgs e)
    {
        timer.Stop();
    }

    private void GatherFoodClick(object sender, RoutedEventArgs e)
    {
        GameResources.Food().Value += 1;
    }

    private void RefineFoodClick(object sender, RoutedEventArgs e)
    {
        GameResources.Food().Value -= 100;
        GameResources.Wood().Value += 1;
    }
}