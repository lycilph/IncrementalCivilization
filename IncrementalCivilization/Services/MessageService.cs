using CommunityToolkit.Mvvm.Messaging;
using IncrementalCivilization.Messages;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;

public class MessageService : IMessageService, IRecipient<ShowMessage>
{
    public ObservableCollection<string> Log { get; private set; } = [];

    public MessageService()
    {
        StrongReferenceMessenger.Default.Register(this);
    }

    public void Add(string text) => Log.Add(text);

    public void Receive(ShowMessage message)
    {
        Add(message.Text);
    }
}
