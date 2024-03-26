namespace Sandbox.Domain;

public static class BuildingsExtensions
{
    public static BuildingsBundle AllBuildings(ResourcesBundle resources)
    {
        var bundle = new BuildingsBundle();

        var field = new BuildingItem(BuildingItemType.Field)
        {
            CostIncrease = 1.12
        };
        field.Cost.Add(new CostItem(resources.Food(), 10));
        bundle.Add(field);

        return bundle;
    }

    public static BuildingItem Field(this BuildingsBundle bundle)
    {
        return bundle[BuildingItemType.Field];
    }
}
