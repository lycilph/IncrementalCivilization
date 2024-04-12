using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class Research
{
    public Improvement Calendar { get; private set; }
    public Improvement Agriculture { get; private set; }
    public Improvement Mining { get; private set; }

    public ObservableCollection<Improvement> Unlocked { get; private set; } = [];
    public ObservableCollection<Improvement> Bought { get; private set; } = [];

    public Research(Game game)
    {
        Calendar = new Improvement("Calendar", "Enables Agriculture");
        Calendar.Cost.Add(new Cost(game.Resources.Science, 30));
        Calendar.BuyAction = () => game.Research.Unlocked.Add(Agriculture!);

        Agriculture = new Improvement("Agriculture", "Enables the farming job");
        Agriculture.Cost.Add(new Cost(game.Resources.Science, 100));
        Agriculture.BuyAction = () => 
        {
            game.Jobs.Farmer.Active = true;
            SendMessage("Farming is now possible");

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

    public static void SendMessage(string msg)
    {
        StrongReferenceMessenger.Default.Send(new AddMessageLogMessage(msg));
    }
}
