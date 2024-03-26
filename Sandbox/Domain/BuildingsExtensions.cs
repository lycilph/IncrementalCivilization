namespace Sandbox.Domain;

public static class BuildingsExtensions
{
    public static BuildingsBundle AllBuildings()
    {
        var bundle = new BuildingsBundle();

        var field = new BuildingItem(BuildingItemType.Field);
        bundle.Add(field);

        return bundle;
    }

    public static BuildingItem Field(this BuildingsBundle bundle)
    {
        return bundle[BuildingItemType.Field];
    }
}
