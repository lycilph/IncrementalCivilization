using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.Domain;

public class ProgressEventManager
{
    private static void ShowMessage(string text) => StrongReferenceMessenger.Default.Send(new ShowMessage(text));
 
    public ObservableCollection<ProgessEvent> Events { get; private set; }

    public ProgressEventManager(Game game)
    {
        Events = [
            new()
            {
                Name = "Start",
                Effect = () => ShowMessage("Your civilization has started its long journey..."),
                Trigger = () => true
            },
            new()
            {
                Name = "Unlock Refine Food",
                Effect = () =>
                {
                    ShowMessage("Maybe food can be used for other things?");
                    game.Capabilities.RefineFoodEnabled = true;
                },
                Trigger = () => game.Resources.Food.Value >= 25
            },
            new()
            {
                Name = "More Wood Message",
                Effect = () => ShowMessage("Try collecting a little more wood"),
                Trigger = () => game.Resources.Wood.Value > 0
            },
            new()
            {
                Name = "Population Increase Message",
                Effect = () => ShowMessage("Over time people will join your village, be patient"),
                Trigger = () => game.Buildings.Hut.Count > 0
            },
            new()
            {
                Name = "Enable Wood Cutters",
                Effect = () =>
                {
                    ShowMessage("Wood cutters are now available");
                    game.Jobs.WoodCutter.Active = true;
                },
                Trigger = () => game.Resources.Population.Value > 0
            },
            new()
            {
                Name = "Enable Scholars",
                Effect = () =>
                {
                    ShowMessage("What wonders to discover...");
                    game.Jobs.Scholar.Active = true;
                },
                Trigger = () => game.Buildings.Library.Count > 0,
            },
            new()
            {
                Effect = () =>
                {
                    ShowMessage("The first idea presents itself");
                    game.Capabilities.ResearchPageEnabled = true;
                    game.Research.Unlocked.Add(game.Research.Calendar);
                },
                Trigger = () => game.Jobs.Scholar.Count > 0,
            },
            new()
            {
                Name = "Enable Miners",
                Effect = () =>
                {
                    ShowMessage("Diggy diggy...");
                    game.Jobs.Miner.Active = true;
                },
                Trigger = () => game.Buildings.Mine.Count > 0,
            }];
    }

    public void Clear()
    {
        Events.Clear();
    }

    public void Process()
    {
        foreach (var e in Events.Where(e => e.Trigger()).ToList()) 
        {
            e.Effect();
            Events.Remove(e);
        }
    }
}
