using System.Collections.ObjectModel;

namespace IncrementalCivilization.Mvvm.Services
{
    public interface ILogService
    {
        ObservableCollection<string> Log { get; }

        void Add(string text);
    }
}