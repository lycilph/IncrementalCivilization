using CommunityToolkit.Mvvm.ComponentModel;
using NLog;

namespace IncrementalCivilization.ViewModels.Core;

public partial class ViewModelBase : ObservableObject, IViewModel
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public bool Initialized { get; protected set; } = false;

    [ObservableProperty]
    private bool _enabled = false;


    public virtual void Initialize()
    {
        logger.Debug($"Initialize {GetType().Name}");

        Initialized = true;
    }

    public virtual void Activate()
    {
        logger.Debug($"Activate {GetType().Name}");

        if (!Initialized)
            Initialize();
    }

    public virtual void Deactivate()
    {
        logger.Debug($"Deactivate {GetType().Name}");
    }
}
