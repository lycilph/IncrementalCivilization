using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;

public interface IMessageLog
{
    ObservableCollection<string> Log { get; }

    void Add(string text);
}