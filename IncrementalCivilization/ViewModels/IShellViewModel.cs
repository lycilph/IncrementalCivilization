using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.ViewModels;

public interface IShellViewModel : IViewModel
{
    public IViewModel? CurrentScreen { get; set; }
}
