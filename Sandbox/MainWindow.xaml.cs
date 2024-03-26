using Sandbox.Domain;
using Sandbox.Utils;
using Sandbox.ViewModels;
using System.Windows;
using System.Windows.Threading;

namespace Sandbox;

public partial class MainWindow : Window
{
    private DispatcherTimer timer;

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
        GameBuildings = BuildingsExtensions.AllBuildings();
        GameJobs = JobsExtensions.AllJobs();

        BuildingsVM = new BuildingsBundleViewModel(GameBuildings, GameResources);
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        GameResources.Food().Value += 0.01;
        GameResources.Wood().Value += 0.05;

        GameResources.Apply(i => i.Limit());
    }

    private void AddFoodClick(object sender, RoutedEventArgs e)
    {
        GameResources.Food().Value += 1;
    }

    private void AddWoodClick(object sender, RoutedEventArgs e)
    {
        GameResources.Wood().Value += 1;
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
}