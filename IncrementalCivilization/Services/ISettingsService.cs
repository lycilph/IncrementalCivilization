using System.ComponentModel;

namespace IncrementalCivilization.Services;

public interface ISettingsService : INotifyPropertyChanged, INotifyPropertyChanging
{
    bool Debug { get; set; }
}