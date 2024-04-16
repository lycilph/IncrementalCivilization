using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Properties;

namespace IncrementalCivilization.Services;

public class SettingsService : ObservableObject, ISettingsService
{
    public bool Debug
    {
        get => Settings.Default.Debug;
        set => SetProperty(Settings.Default.Debug, value, Settings.Default, (u, v) => u.Debug = v);
    }
}
