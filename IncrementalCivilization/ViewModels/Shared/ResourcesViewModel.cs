using IncrementalCivilization.Domain;
using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.ViewModels.Shared;

public class ResourcesViewModel(Game game) : ViewModelBase
{
    public IEnumerable<Resource> Resources { get => game.Resources; }
}
