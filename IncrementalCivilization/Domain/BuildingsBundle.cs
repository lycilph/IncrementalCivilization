namespace IncrementalCivilization.Domain;

public class BuildingsBundle : ItemsBundle<BuildingItemType, BuildingItem>
{
    public BuildingItem Field { get; private set; } = new(BuildingItemType.Field);
    public BuildingItem Hut { get; private set; } = new(BuildingItemType.Hut);

    public static BuildingsBundle AllBuildings(ResourcesBundle resources)
    {
        var bundle = new BuildingsBundle();

        bundle.Field = new BuildingItem(BuildingItemType.Field)
        {
            CostIncrease = 1.12
        };
        bundle.Field.Cost.Add(new CostItem(resources.Food, 10));
        bundle.Add(bundle.Field);

        bundle.Hut = new BuildingItem(BuildingItemType.Hut)
        {
            CostIncrease = 2.5
        };
        bundle.Hut.Cost.Add(new CostItem(resources.Wood, 5));
        bundle.Hut.BuyAction = () => resources.Population.Maximum += 2;
        bundle.Add(bundle.Hut);

        return bundle;
    }
}
