using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class Upgrades
{
    public Improvement WoodHoe { get; private set; }

    public ObservableCollection<Improvement> Unlocked { get; private set; } = [];
    public ObservableCollection<Improvement> Bought { get; private set; } = [];

    public Upgrades(Game game)
    {
        WoodHoe = new Improvement("Wood Hoe", "A wooden hoe, improves farmer efficiency");
        WoodHoe.Cost.Add(new Cost(game.Resources.Science, 50));
        WoodHoe.Cost.Add(new Cost(game.Resources.Wood, 25));
        WoodHoe.BuyAction = () => game.Effects.FarmerEffieciency += 0.1;
    }
}
