using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace IncrementalCivilization.ViewModels;

public partial class ViewModelBase(ILogger logger) : ObservableObject, IViewModel
{
    protected readonly ILogger _logger = logger;

    public bool Initialized { get; protected set; } = false;

    public virtual void Initialize()
    {
        _logger.LogInformation("ViewModel {vm} is initialized", GetType());

        Initialized = true;
    }
}
