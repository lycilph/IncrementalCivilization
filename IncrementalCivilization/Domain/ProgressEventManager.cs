using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;

namespace IncrementalCivilization.Domain;

public class ProgressEventManager
{
    private readonly List<ProgessEvent> events;

    private void Send(string text) => StrongReferenceMessenger.Default.Send(new ShowMessage(text));

    public ProgressEventManager(Game game)
    {
        events = [
            new()
            {
                Effect = () => Send("Your civilization has started its long journey..."),
                Trigger = () => true
            },
            new()
            {
                Effect = () =>
                {
                    Send("Maybe food can be used for other things?");
                    game.Capabilities.RefineFoodEnabled = true;
                },
                Trigger = () => game.Resources.Food.Value >= 25
            }];
    }

    public void Clear()
    {
        events.Clear();
    }

    public void Process()
    {
        foreach (var e in events.Where(e => e.Trigger()).ToList()) 
        {
            e.Effect();
            events.Remove(e);
        }
    }
}
