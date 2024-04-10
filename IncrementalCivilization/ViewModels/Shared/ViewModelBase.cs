using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class ViewModelBase : ObservableObject, IViewModel
{
    public bool Initialized { get; protected set; } = false;

    [ObservableProperty]
    private bool _enabled = false;

    public virtual void Initialize()
    {
        Initialized = true;
    }
}
