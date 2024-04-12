using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;

namespace IncrementalCivilization.Domain;

public static class ProgressEvents
{
    public static void SendMessage(string msg)
    {
        StrongReferenceMessenger.Default.Send(new AddMessageLogMessage(msg));
    }

    public static List<ProgessEvent> Initialize(Game game)
    {
        return
        [
            new()
            {
                Effect = () =>
                {
                    SendMessage("Maybe food can be used for other things?");
                    game.Capabilities.RefineFoodEnabled = true;
                },
                Trigger = () => game.Resources.Food.Value >= 25
            },
            new()
            {
                Effect = () => SendMessage("Try collecting a little more wood"),
                Trigger = () => game.Resources.Wood.Value > 0
            },
            new()
            {
                Effect = () => SendMessage("Over time people will join your village, be patient"),
                Trigger = () => game.Buildings.Hut.Count > 0
            },
            new()
            {
                Effect = () => game.Jobs.WoodCutter.Active = true,
                Trigger = () => game.Resources.Population.Value > 0
            },
            new()
            {
                Effect = () =>
                {
                    game.Jobs.Scholar.Active = true;
                    SendMessage("What wonders to discover...");
                },
                Trigger = () => game.Buildings.Library.Count > 0,
            },
            new()
            {
                Effect = () =>
                {
                    game.Capabilities.ResearchPageEnabled = true;
                    game.Research.Unlocked.Add(game.Research.Calendar);
                    SendMessage("The first idea presents itself");
                },
                Trigger = () => game.Jobs.Scholar.Count > 0,
            },
        ];
    }
}
