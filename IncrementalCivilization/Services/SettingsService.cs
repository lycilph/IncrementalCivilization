using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;

public enum OptionType { DebugFood, DebugWood };

public partial class Option(OptionType type, string name, string value) : ObservableObject
{
    public OptionType Type { get; private set; } = type;

    [ObservableProperty]
    private string _name = name;

    [ObservableProperty]
    private string _value = value;
}

public class SettingsService : ISettingsService
{
    public ObservableCollection<Option> Options { get; private set; } = [];

    public SettingsService()
    {
        Options =
        [
            new(OptionType.DebugFood, "Food To Add", "1000"),
            new(OptionType.DebugWood, "Wood To Add", "100")
        ];
    }

    public Option Get(OptionType type)
    {
        return Options.First(o => o.Type == type);
    }
}
