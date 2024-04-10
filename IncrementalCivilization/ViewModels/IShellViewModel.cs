using IncrementalCivilization.ViewModels.Shared;

namespace IncrementalCivilization.ViewModels;

public interface IShellViewModel : IViewModel
{
    public IViewModel? CurrentScreen { get; set; }
}
