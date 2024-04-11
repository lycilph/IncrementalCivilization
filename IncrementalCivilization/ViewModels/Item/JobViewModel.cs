using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;

namespace IncrementalCivilization.ViewModels.Item;

public partial class JobViewModel : ObservableObject
{
    private readonly Job _job;
    private readonly Resource _freePopulation;

    public string Name { get => _job.Name; }
    public int Count { get => _job.Count; }
    public bool Active { get => _job.Active; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    private bool canRemove = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    private bool canAdd = false;

    public JobViewModel(Job job, Resource freePopulation)
    {
        _job = job;
        _freePopulation = freePopulation;

        _job.PropertyChanged += (s, e) =>
        {
            OnPropertyChanged(e.PropertyName); // Forward change notifications
            CanRemove = Count > 0;
        };

        _freePopulation.PropertyChanged += (s, e) => CanAdd = freePopulation.Value > 0;
    }


    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        _job.Count += 1;
    }

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        _job.Count -= 1;
    }
}
