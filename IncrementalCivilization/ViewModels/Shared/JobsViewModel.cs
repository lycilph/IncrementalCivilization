using IncrementalCivilization.Domain;
using System.ComponentModel;

namespace IncrementalCivilization.ViewModels.Shared;

public class JobsViewModel : ViewModelBase
{
    private readonly Game game;

    public IEnumerable<Job> Jobs { get => game.Jobs; }
    public Resource FreePopulation { get; private set; } = new Resource(ResourceType.Population);

    public JobsViewModel(Game game)
    {
        this.game = game;

        game.Resources.Population.PropertyChanged += PopulationPropertyChanged;
        foreach (var job in Jobs)
            job.PropertyChanged += PopulationPropertyChanged;
    }

    private void PopulationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        FreePopulation.Maximum = game.Resources.Population.Value;
        FreePopulation.Value = FreePopulation.Maximum - Jobs.Sum(j => j.Count);
    }
}
