namespace Sandbox.Domain;

public static class ResourceExtensions
{
    public static ResourcesBundle AllResources()
    {
        var bundle = new ResourcesBundle();

        var food = new ResourceItem(ResourceItemType.Food)
        {
            Threshold = 10
        };
        bundle.Add(food);

        var wood = new ResourceItem(ResourceItemType.Wood)
        {
            Threshold = 5
        };
        bundle.Add(wood);

        return bundle;
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
