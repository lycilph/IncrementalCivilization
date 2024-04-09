using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class ResearchBundle
{
    public ImprovementItem Agriculture { get; private set; } = new();

    public ObservableCollection<ImprovementItem> Items { get; private set; } = [];

    public void Add(ImprovementItem item) => Items.Add(item);

    public static ResearchBundle Initialize(Game game)
    {
        var bundle = new ResearchBundle();

        var agriculture = new ImprovementItem("Agriculture", "Enables the farming job");
        agriculture.Cost.Add(new CostItem(game.Resources.Science, 50));
        agriculture.BuyAction = () => game.Jobs.Farmer.Active = true;
        bundle.Agriculture = agriculture;

        return bundle;
    }
}
