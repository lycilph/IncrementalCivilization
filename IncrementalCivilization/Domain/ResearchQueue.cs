using IncrementalCivilization.Domain.Core;

namespace IncrementalCivilization.Domain;

public class ResearchQueue : ImprovementQueue
{
    public Improvement Calendar { get; private set; }
    public Improvement Agriculture { get; private set; }
    public Improvement Mining { get; private set; }

    public ResearchQueue(Game game)
    {
        Calendar = new Improvement("Calendar", "Enables Agriculture");
        Calendar.Cost.Add(new Cost(game.Resources.Science, 30));
        Calendar.BuyAction = () => 
        { 
            game.Research.Unlocked.Add(Agriculture!); 
            game.Capabilities.TimePageEnabled = true;
        };

        Agriculture = new Improvement("Agriculture", "Enables the farming job");
        Agriculture.Cost.Add(new Cost(game.Resources.Science, 100));
        Agriculture.BuyAction = () =>
        {
            ShowMessage("Farming is now possible");
            game.Jobs.Farmer.Active = true;
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
    }
}
