using IncrementalCivilization.Domain;

namespace IncrementalCivilization.ViewModels.Shared;

public class ResourcesViewModel(Game game) : ViewModelBase
{
    public IEnumerable<Resource> Resources { get => game.Resources; }
}
