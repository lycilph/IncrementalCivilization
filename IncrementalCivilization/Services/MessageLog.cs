using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;

public class MessageLog : IMessageLog
{
    public ObservableCollection<string> Log { get; private set; } = [];

    public void Add(string text) => Log.Add(text);
}
