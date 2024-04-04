namespace IncrementalCivilization.Domain;

public static class ResourcesExtensions
{
    public static ResourcesBundle AllResources()
    {
        return
        [
            new(ResourceItemType.Population)
            {
                ShowRate = false
            },
            new(ResourceItemType.Food, 1000),
            new(ResourceItemType.Wood, 100),
            new(ResourceItemType.Science, 500)
        ];
    }

    public static ResourceItem Population(this ResourcesBundle bundle)
    {
        return bundle[ResourceItemType.Population];
    }

    public static ResourceItem Food(this ResourcesBundle bundle)
    {
        return bundle[ResourceItemType.Food];
    }

    public static ResourceItem Wood(this ResourcesBundle bundle)
    {
        return bundle[ResourceItemType.Wood];
    }

    public static ResourceItem Science(this ResourcesBundle bundle)
    {
        return bundle[ResourceItemType.Science];
    }
}
