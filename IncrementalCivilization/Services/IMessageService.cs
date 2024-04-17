using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;

public interface IMessageService
{
    ObservableCollection<string> Log { get; }

    void Add(string text);
}