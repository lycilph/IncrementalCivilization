using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Utils;
using IncrementalCivilization.ViewModels.Core;
using IncrementalCivilization.ViewModels.Item;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class JobsViewModel(Game game) : ViewModelBase
{
    public ObservableCollection<JobViewModel> Jobs { get; private set; } = [];
    public Resource FreePopulation { get; private set; } = new Resource(ResourceType.Population);

    [ObservableProperty]
    private bool _showJobs = false;

    public override void Activate()
    {
        base.Activate();

        ShowJobs = false;
        FreePopulation = new Resource(ResourceType.Population);

        Jobs = new ObservableCollection<JobViewModel>(game.Jobs.Select(j => new JobViewModel(j, FreePopulation)));
        Jobs.Apply(j => j.PropertyChanged += UpdateOnPropertyChanged);

        game.Resources.Population.PropertyChanged += UpdateOnPropertyChanged;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        foreach (var j in Jobs) 
        {
            j.PropertyChanged -= UpdateOnPropertyChanged;
            j.Deactivate();
        }
        Jobs.Clear();

        game.Resources.Population.PropertyChanged -= UpdateOnPropertyChanged;
    }

    private void UpdateOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        FreePopulation.Maximum = game.Resources.Population.Value;
        FreePopulation.Value = FreePopulation.Maximum - Jobs.Sum(j => j.Count);
        ShowJobs = Jobs.Any(j => j.Active);
    }

    [RelayCommand]
    private void ClearJobs()
    {
        game.Jobs.Limit(0);
    }
}
