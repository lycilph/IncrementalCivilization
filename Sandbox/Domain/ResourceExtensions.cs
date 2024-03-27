namespace Sandbox.Domain;

public static class ResourceExtensions
{
    public static ResourcesBundle AllResources()
    {
        var bundle = new ResourcesBundle();

        var people = new ResourceItem(ResourceItemType.People);
        bundle.Add(people);

        var food = new ResourceItem(ResourceItemType.Food)
        {
            Threshold = 1000
        };
        bundle.Add(food);

        var wood = new ResourceItem(ResourceItemType.Wood)
        {
            Threshold = 100
        };
        bundle.Add(wood);

        return bundle;
    }

    public static ResourcesBundle SingleResource(ResourceItem item)
    {
        return
        [
            item
        ];
    }

    public static ResourceItem People(this ResourcesBundle bundle)
    {
        return bundle[ResourceItemType.People];
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
