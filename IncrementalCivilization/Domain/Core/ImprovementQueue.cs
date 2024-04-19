using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using IncrementalCivilization.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace IncrementalCivilization.Domain.Core;

public class ImprovementQueue
{
    protected static void ShowMessage(string text) => StrongReferenceMessenger.Default.Send(new ShowMessage(text));

    public ObservableCollection<Improvement> Unlocked { get; private set; } = [];
    public ObservableCollection<Improvement> Bought { get; private set; } = [];

    public ImprovementQueue()
    {
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

    public virtual void Clear()
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
