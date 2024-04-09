using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class UpgradesBundle
{
    public ImprovementItem WoodHoe { get; private set; } = new();
    public ImprovementItem WoodSaw { get; private set; } = new();
    public ImprovementItem WoodDesk { get; private set; } = new();

    public ObservableCollection<ImprovementItem> Items { get; private set; } = [];

    public void Add(ImprovementItem item) => Items.Add(item);

    public static UpgradesBundle Initialize(Game game)
    {
        var bundle = new UpgradesBundle();

        var woodHoe = new ImprovementItem("Wood Hoe", "A wooden hoe, improves farmer efficiency");
        woodHoe.Cost.Add(new CostItem(game.Resources.Science, 50));
        woodHoe.BuyAction = () => game.Effects.FarmerEffieciency += 0.1;
        bundle.WoodHoe = woodHoe;

        var woodSaw = new ImprovementItem("Wood Saw", "A wooden saw, improves wood cutters efficiency");
        woodSaw.Cost.Add(new CostItem(game.Resources.Science, 100));
        woodSaw.BuyAction = () => game.Effects.WoodCutterEffieciency += 0.1;
        bundle.WoodSaw = woodSaw;

        var woodDesk = new ImprovementItem("Wood Desk", "A wooden desk, improves scholars efficiency");
        woodDesk.Cost.Add(new CostItem(game.Resources.Science, 200));
        woodDesk.BuyAction = () => game.Effects.ScholarEffieciency += 0.1;
        bundle.WoodDesk = woodDesk;

        return bundle;
    }
}
