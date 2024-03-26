namespace Sandbox.Domain;

public static class BuildingsExtensions
{
    public static BuildingsBundle AllBuildings()
    {
        var bundle = new BuildingsBundle();

        var food = new ResourceItem(ResourceItemType.Food)
        {
            Threshold = 10
        };
        var field = new BuildingItem(BuildingItemType.Field)
        {
            Cost = ResourceExtensions.SingleResource(food),
            CostIncrease = 1.12
        };
        bundle.Add(field);

        return bundle;
    }

    public static BuildingItem Field(this BuildingsBundle bundle)
    {
        return bundle[BuildingItemType.Field];
    }
}
