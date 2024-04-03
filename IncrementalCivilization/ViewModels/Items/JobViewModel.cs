using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;

namespace IncrementalCivilization.ViewModels.Items;

public partial class JobViewModel : ObservableObject
{
    public JobItem Item { get; private set; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    private bool canRemove = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    private bool canAdd = false;

    public JobViewModel(JobItem item, ResourceItem freePopulation)
    {
        Item = item;

        Item.PropertyChanged += (s, e) =>
        {
            CanRemove = Item.Count > 0;
        };

        freePopulation.PropertyChanged += (s, e) =>
        {
            CanAdd = freePopulation.Value > 0;
        };
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        Item.Count += 1;
    }

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        Item.Count -= 1;
    }
}
