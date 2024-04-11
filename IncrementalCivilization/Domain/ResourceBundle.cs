using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class ResourceBundle : Bundle<ResourceType, Resource>
{
    public Resource Population { get; private set; }
    public Resource Food { get; private set; }
    public Resource Wood { get; private set; }
    public Resource Science { get; private set; }

    public void Limit()
    {
        this.Apply(i => i.Limit());
    }

    public ResourceBundle()
    {
        Population = new Resource(ResourceType.Population) { ShowRate = false, ShowAsInterger = true };
        Food = new Resource(ResourceType.Food, 1000);
        Wood = new Resource(ResourceType.Wood, 100);
        Science = new Resource(ResourceType.Science, 500);
        
        Add(Population, Food, Wood, Science);
    }
}
