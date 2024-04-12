using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class ResourceBundle : Bundle<ResourceType, Resource>
{
    public Resource Population { get; private set; }
    public Resource Food { get; private set; }
    public Resource Wood { get; private set; }
    public Resource Minerals { get; private set; }
    public Resource Science { get; private set; }

    public void Limit()
    {
        this.Apply(i => i.Limit());
    }

    public ResourceBundle()
    {
        Population = new Resource(ResourceType.Population) { ShowRate = false, ShowAsInterger = true };
        Food = new Resource(ResourceType.Food, 5000);
        Wood = new Resource(ResourceType.Wood, 200);
        Minerals = new Resource(ResourceType.Minerals, 200);
        Science = new Resource(ResourceType.Science, 250);
        
        Add(Population, Food, Wood, Minerals, Science);
    }
}
