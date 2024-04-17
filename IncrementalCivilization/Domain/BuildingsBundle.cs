using IncrementalCivilization.Domain.Core;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class BuildingsBundle : Bundle<BuildingType, Building>
{
    #region Buildings
    public Building Field { get; private set; }
    public Building Hut { get; private set; }
    public Building Library { get; private set; }
    public Building Barn { get; private set; }
    public Building Mine { get; private set; }
    public Building Workshop { get; private set; }
    #endregion

    public BuildingsBundle(ResourceBundle resources)
    {
        Field = new Building(BuildingType.Field) { CostIncrease = 1.12 };
        Field.Cost.Add(new Cost(resources.Food, 10));

        Hut = new Building(BuildingType.Hut) { CostIncrease = 2.5 };
        Hut.Cost.Add(new Cost(resources.Wood, 5));
        Hut.BuyAction = () => resources.Population.Maximum += 2;

        Library = new Building(BuildingType.Library);
        Library.Cost.Add(new Cost(resources.Wood, 25));
        Library.BuyAction = () => resources.Science.Maximum += 250;

        Barn = new Building(BuildingType.Barn) { CostIncrease = 1.75 };
        Barn.Cost.Add(new Cost(resources.Wood, 50));
        Barn.BuyAction = () =>
        {
            resources.Food.Maximum += 5000;
            resources.Wood.Maximum += 200;
            resources.Minerals.Maximum += 250;
        };

        Mine = new Building(BuildingType.Mine);
        Mine.Cost.Add(new Cost(resources.Wood, 100));

        Workshop = new Building(BuildingType.Workshop);
        Workshop.Cost.Add(new Cost(resources.Wood, 100));
        Workshop.Cost.Add(new Cost(resources.Minerals, 400));

        Add(Field, Hut, Library);
    }

    public override void Clear()
    {
        this.Apply(b => b.Clear());
        base.Clear();
    }
}