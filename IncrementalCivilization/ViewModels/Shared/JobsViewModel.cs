using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain;
using IncrementalCivilization.ViewModels.Item;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class JobsViewModel : ViewModelBase
{
    private readonly Game game;

    public ObservableCollection<JobViewModel> Jobs { get; private set; }
    public Resource FreePopulation { get; private set; } = new Resource(ResourceType.Population);

    [ObservableProperty]
    private bool _showJobs = false;

    public JobsViewModel(Game game)
    {
        this.game = game;

        Jobs = new ObservableCollection<JobViewModel>(game.Jobs.Select(j => new JobViewModel(j, FreePopulation)));

        game.Resources.Population.PropertyChanged += PopulationPropertyChanged;
        foreach (var job in Jobs)
            job.PropertyChanged += PopulationPropertyChanged;
    }

    private void PopulationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        FreePopulation.Maximum = game.Resources.Population.Value;
        FreePopulation.Value = FreePopulation.Maximum - Jobs.Sum(j => j.Count);

        ShowJobs = Jobs.Any(j => j.Active);
    }
}
