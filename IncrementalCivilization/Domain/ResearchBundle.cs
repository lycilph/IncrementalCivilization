using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class ResearchBundle
{
    public ObservableCollection<ResearchItem> Items { get; private set; } = [];

    public void Add(ResearchItem item) => Items.Add(item);

    public static ResearchBundle AllResearch(Game game)
    {
        var bundle = new ResearchBundle();

        var agriculture = new ResearchItem("Agriculture", "Enables the farming job");
        agriculture.Cost.Add(new CostItem(game.Resources.Science, 50));
        agriculture.BuyAction = () => game.Jobs.Farmer.Active = true;
        bundle.Add(agriculture);

        //var betterFarmers = new ResearchItem("Better Farmes", "Increases farmer efficiency");
        //betterFarmers.Cost.Add(new CostItem(game.Resources.Science, 50));
        //betterFarmers.BuyAction = () => game.Effects.FarmerEffieciency += 0.1;
        //bundle.Add(betterFarmers);

        //var betterWoodCutters = new ResearchItem("Better Wood Cutters", "Increases wood cutter efficiency");
        //betterWoodCutters.Cost.Add(new CostItem(game.Resources.Science, 100));
        //betterWoodCutters.BuyAction = () => game.Effects.WoodCutterEffieciency += 0.1;
        //bundle.Add(betterWoodCutters);

        //var betterScholars = new ResearchItem("Better Scholars", "Increases scholar efficiency");
        //betterScholars.Cost.Add(new CostItem(game.Resources.Science, 200));
        //betterScholars.BuyAction = () => game.Effects.ScholarEffieciency += 0.1;
        //bundle.Add(betterScholars);

        return bundle;
    }
}
