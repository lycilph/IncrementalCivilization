using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;

namespace IncrementalCivilization.Domain;

public class ProgressEventManager
{
    private List<ProgessEvent> events;

    private void Send(string text) => StrongReferenceMessenger.Default.Send(new ShowMessage(text));

    public ProgressEventManager()
    {
        events = [
            new()
            {
                Effect = () => Send("Your civilization has started its long journey..."),
                Trigger = () => true
            }];
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
