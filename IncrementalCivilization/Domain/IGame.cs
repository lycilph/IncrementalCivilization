using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public interface IGame
{
    Bundle<ResourceType, Resource> Resources { get; }
}