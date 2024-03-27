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

        var hut = new BuildingItem(BuildingItemType.Hut)
        {
            CostIncrease = 1.15
        };
        hut.Cost.Add(new CostItem(resources.Wood(), 5));
        hut.BuyAction = () => resources.Population().Threshold += 2;
        bundle.Add(hut);

        return bundle;
    }

    public static BuildingItem Field(this BuildingsBundle bundle)
    {
        return bundle[BuildingItemType.Field];
    }

    public static BuildingItem Hut(this BuildingsBundle bundle)
    {
        return bundle[BuildingItemType.Hut];
    }
}
