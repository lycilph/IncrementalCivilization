namespace Sandbox.Domain;

public static class ResourceExtensions
{
    public static ResourcesBundle AllResources()
    {
        var bundle = new ResourcesBundle();

        var population = new ResourceItem(ResourceItemType.Population)
        {
            ShowRate = false
        };
        bundle.Add(population);

        var food = new ResourceItem(ResourceItemType.Food)
        {
            Maximum = 1000
        };
        bundle.Add(food);

        var wood = new ResourceItem(ResourceItemType.Wood)
        {
            Maximum = 100
        };
        bundle.Add(wood);

        return bundle;
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
}
