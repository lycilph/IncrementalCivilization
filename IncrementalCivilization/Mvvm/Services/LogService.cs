using System.Collections.ObjectModel;

namespace IncrementalCivilization.Mvvm.Services;

public class LogService : ILogService
{
    public ObservableCollection<string> Log { get; private set; } = [];

    public void Add(string text) => Log.Add(text);
}
