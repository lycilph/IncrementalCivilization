using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace IncrementalCivilization.Domain;

public class ResearchQueue
{
    private static void ShowMessage(string text) => StrongReferenceMessenger.Default.Send(new ShowMessage(text));

    public Improvement Calendar { get; private set; }
    public Improvement Agriculture { get; private set; }
    public Improvement Mining { get; private set; }

    public ObservableCollection<Improvement> Unlocked { get; private set; } = [];
    public ObservableCollection<Improvement> Bought { get; private set; } = [];

    public ResearchQueue(Game game)
    {
        Calendar = new Improvement("Calendar", "Enables Agriculture");
        Calendar.Cost.Add(new Cost(game.Resources.Science, 30));
        Calendar.BuyAction = () => game.Research.Unlocked.Add(Agriculture!);

        Agriculture = new Improvement("Agriculture", "Enables the farming job");
        Agriculture.Cost.Add(new Cost(game.Resources.Science, 100));
        Agriculture.BuyAction = () =>
        {
            game.Jobs.Farmer.Active = true;
            ShowMessage("Farming is now possible");

            game.Buildings.Add(game.Buildings.Barn);

            game.Research.Unlocked.Add(Mining!);
        };

        Mining = new Improvement("Mining", "Enables mines and the workshop");
        Mining.Cost.Add(new Cost(game.Resources.Science, 500));
        Mining.BuyAction = () =>
        {
            game.Buildings.Add(game.Buildings.Mine);
            game.Buildings.Add(game.Buildings.Workshop);
        };

        Unlocked.CollectionChanged += UnlockedCollectionChanged;
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
