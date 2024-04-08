using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class ResearchBundle
{
    public ObservableCollection<ResearchItem> Items { get; private set; } = [];

    public void Add(ResearchItem item) => Items.Add(item);
}
