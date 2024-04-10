using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class ResourceManager
{
    public static Bundle<ResourceType, Resource> AllResources()
    {
        return
        [
            new Resource(ResourceType.Population),
            new Resource(ResourceType.Food, 1000),
            new Resource(ResourceType.Wood, 100),
            new Resource(ResourceType.Science, 500)
        ];
    }
}
