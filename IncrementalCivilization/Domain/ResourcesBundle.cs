namespace IncrementalCivilization.Domain;

public class ResourcesBundle : ItemsBundle<ResourceItemType, ResourceItem>
{
    public ResourceItem Population { get; private set; } = new(ResourceItemType.Population);
    public ResourceItem Food { get; private set; } = new(ResourceItemType.Food);
    public ResourceItem Wood { get; private set; } = new(ResourceItemType.Wood);
    public ResourceItem Science { get; private set; } = new(ResourceItemType.Science);

    public static ResourcesBundle AllResources()
    {
        var bundle = new ResourcesBundle
        {
            Population = new ResourceItem(ResourceItemType.Population) { ShowRate = false },
            Food = new(ResourceItemType.Food, 1000),
            Wood = new(ResourceItemType.Wood, 100),
            Science = new(ResourceItemType.Science, 500)
        };

        bundle.Add(bundle.Population);
        bundle.Add(bundle.Food);
        bundle.Add(bundle.Wood);
        bundle.Add(bundle.Science);

        return bundle;
    }
}
