using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class Upgrades
{
    public Improvement MineralHoe { get; private set; }
    public Improvement MineralAxe { get; private set; }

    public ObservableCollection<Improvement> Unlocked { get; private set; } = [];
    public ObservableCollection<Improvement> Bought { get; private set; } = [];

    public Upgrades(Game game)
    {
        MineralHoe = new Improvement("Mineral Hoe", "Farmer are 50% more efficient");
        MineralHoe.Cost.Add(new Cost(game.Resources.Science, 100));
        MineralHoe.Cost.Add(new Cost(game.Resources.Minerals, 275));
        MineralHoe.BuyAction = () => game.Effects.FarmerEffieciency += 0.5;

        MineralAxe = new Improvement("Mineral Axe", "Wood cutters are 70% more efficient");
        MineralAxe.Cost.Add(new Cost(game.Resources.Science, 100));
        MineralAxe.Cost.Add(new Cost(game.Resources.Minerals, 500));
        MineralAxe.BuyAction = () => game.Effects.WoodCutterEffieciency += 0.7;

        Unlocked.Add(MineralHoe);
        Unlocked.Add(MineralAxe);
    }
}
