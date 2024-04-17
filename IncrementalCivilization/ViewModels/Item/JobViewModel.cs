using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.ViewModels.Item;

public partial class JobViewModel : ViewModelBase
{
    private readonly Job _job;
    private readonly Resource _freePopulation;

    public string Name { get => _job.Name; }
    public int Count { get => _job.Count; }
    public bool Active { get => _job.Active; }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveAllCommand))]
    private bool canRemove = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [NotifyCanExecuteChangedFor(nameof(AddAllCommand))]
    private bool canAdd = false;

    public JobViewModel(Job job, Resource freePopulation)
    {
        _job = job;
        _freePopulation = freePopulation;

        _job.PropertyChanged += UpdateOnPropertyChanged;
        _freePopulation.PropertyChanged += UpdateOnPropertyChanged;
    }

    private void UpdateOnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender == _job)
            OnPropertyChanged(e.PropertyName); // Forward change notifications

        CanRemove = Count > 0;
        CanAdd = _freePopulation.Value > 0;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        _job.PropertyChanged -= UpdateOnPropertyChanged;
        _freePopulation.PropertyChanged -= UpdateOnPropertyChanged;
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void Add()
    {
        _job.Count += 1;
    }

    [RelayCommand(CanExecute = nameof(CanAdd))]
    private void AddAll()
    {
        _job.Count += (int)_freePopulation.Value;
    }

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void Remove()
    {
        _job.Count -= 1;
    }

    [RelayCommand(CanExecute = nameof(CanRemove))]
    private void RemoveAll()
    {
        _job.Count = 0;
    }
}
