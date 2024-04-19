using IncrementalCivilization.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace IncrementalCivilization.Domain;

public class UpgradesQueue
{
    public Improvement MineralHoe { get; private set; }
    public Improvement MineralAxe { get; private set; }

    public ObservableCollection<Improvement> Unlocked { get; private set; } = [];
    public ObservableCollection<Improvement> Bought { get; private set; } = [];

    public UpgradesQueue(Game game)
    {
        MineralHoe = new Improvement("Mineral Hoe", "Farmer are 50% more efficient");
        MineralHoe.Cost.Add(new Cost(game.Resources.Science, 100));
        MineralHoe.Cost.Add(new Cost(game.Resources.Minerals, 275));
        MineralHoe.BuyAction = () => game.Effects.FarmerEffieciency += 0.5;

        MineralAxe = new Improvement("Mineral Axe", "Wood cutters are 70% more efficient");
        MineralAxe.Cost.Add(new Cost(game.Resources.Science, 100));
        MineralAxe.Cost.Add(new Cost(game.Resources.Minerals, 500));
        MineralAxe.BuyAction = () => game.Effects.WoodCutterEffieciency += 0.7;

        Unlocked.CollectionChanged += UnlockedCollectionChanged;

        Unlocked.Add(MineralHoe);
        Unlocked.Add(MineralAxe);
    }

    private void UnlockedCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            foreach (var i in e.NewItems)
                if (i is Improvement improvement && improvement != null)
                    improvement.PropertyChanged += ImprovementPropertyChanged;
    }

    private void ImprovementPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is Improvement improvement && improvement.IsBought)
        {
            improvement.PropertyChanged -= ImprovementPropertyChanged;
            Unlocked.Remove(improvement);
            Bought.Add(improvement);
        }
    }

    public void Clear()
    {
        Unlocked.CollectionChanged -= UnlockedCollectionChanged;
        foreach (var improvement in Unlocked)
        {
            improvement.PropertyChanged -= ImprovementPropertyChanged;
            improvement.Clear();
        }
        Unlocked.Clear();

        Bought.Apply(i => i.Clear());
        Bought.Clear();
    }
}
