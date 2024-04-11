using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class BuildingsBundle : Bundle<BuildingType, Building>
{
    public Building Field { get; private set; }
    public Building Hut { get; private set; }
    public Building Library { get; private set; }
    public Building Test { get; private set; }

    public BuildingsBundle(ResourceBundle resources)
    {
        Field = new Building(BuildingType.Field) { CostIncrease = 1.12, Active = true };
        Field.Cost.Add(new Cost(resources.Food, 10));

        Hut = new Building(BuildingType.Hut) { CostIncrease = 2.5, Active = true };
        Hut.Cost.Add(new Cost(resources.Wood, 5));

        Library = new Building(BuildingType.Library) { Active = true };
        Library.Cost.Add(new Cost(resources.Wood, 25));

        Test = new Building(BuildingType.Test) { Active = true };
        Test.Cost.Add(new Cost(resources.Food, 50));
        Test.Cost.Add(new Cost(resources.Wood, 10));

        Add(Field, Hut, Library, Test);
    }
}
