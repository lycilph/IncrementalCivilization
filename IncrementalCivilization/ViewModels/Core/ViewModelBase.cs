using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.ViewModels.Core;

public partial class ViewModelBase : ObservableObject, IViewModel
{
    public bool Initialized { get; protected set; } = false;

    [ObservableProperty]
    private bool _enabled = false;

    public virtual void Initialize()
    {
        Initialized = true;
    }

    public virtual void Activate()
    {
        if (!Initialized)
            Initialize();
    }

    public virtual void Deactivate() {}
}
