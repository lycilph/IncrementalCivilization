using CommunityToolkit.Mvvm.ComponentModel;
using Sandbox.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Sandbox.ViewModels;

public partial class JobsBundleViewModel : ObservableObject
{
    private readonly ResourceItem population;

    public ObservableCollection<JobsItemViewModel> Items { get; private set; }

    public ResourceItem FreePopulation { get; private set; } = new(ResourceItemType.Population);

    public JobsBundleViewModel(IEnumerable<JobItem> items, ResourceItem population)
    {
        Items = new ObservableCollection<JobsItemViewModel>(items.Select(x => new JobsItemViewModel(x, FreePopulation)));
        this.population = population;

        population.PropertyChanged += PopulationPropertyChanged;
        foreach (var item in items)
            item.PropertyChanged += PopulationPropertyChanged;
    }

    private void PopulationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        FreePopulation.Maximum = population.Value;
        FreePopulation.Value = FreePopulation.Maximum - Items.Aggregate(0, (a, b) => a + b.Item.Count);
    }
}
