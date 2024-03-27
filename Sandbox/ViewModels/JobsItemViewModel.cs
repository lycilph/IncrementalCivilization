using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sandbox.Domain;

namespace Sandbox.ViewModels;

public partial class JobsItemViewModel : ObservableObject
{
    public JobItem Item { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    private bool canRemove = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    private bool canAdd = false;

    public JobsItemViewModel(JobItem item, ResourceItem freePopulation)
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
