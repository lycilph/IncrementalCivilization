using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class ResearchBundle
{
    public ObservableCollection<ResearchItem> Items { get; private set; } = [];

    public void Add(ResearchItem item) => Items.Add(item);

    public static ResearchBundle AllResearch(ResourcesBundle resources, Effects effects)
    {
        var bundle = new ResearchBundle();

        var betterFarmers = new ResearchItem("Better Farmes", "Increases farmer efficiency");
        betterFarmers.Cost.Add(new CostItem(resources.Science, 50));
        betterFarmers.BuyAction = () => effects.FarmerEffieciency += 0.1;
        bundle.Add(betterFarmers);

        var betterWoodCutters = new ResearchItem("Better Wood Cutters", "Increases wood cutter efficiency");
        betterWoodCutters.Cost.Add(new CostItem(resources.Science, 100));
        betterWoodCutters.BuyAction = () => effects.WoodCutterEffieciency += 0.1;
        bundle.Add(betterWoodCutters);

        var betterScholars = new ResearchItem("Better Scholars", "Increases scholar efficiency");
        betterScholars.Cost.Add(new CostItem(resources.Science, 200));
        betterScholars.BuyAction = () => effects.ScholarEffieciency += 0.1;
        bundle.Add(betterScholars);

        return bundle;
    }
}
