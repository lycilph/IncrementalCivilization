using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;

public class MessageLog : IMessageLog, IRecipient<AddMessageLogMessage>
{
    public ObservableCollection<string> Log { get; private set; } = [];

    public MessageLog()
    {
        StrongReferenceMessenger.Default.Register(this);
    }

    public void Add(string text) => Log.Add(text);

    public void Receive(AddMessageLogMessage message)
    {
        Add(message.Text);
    }
}
