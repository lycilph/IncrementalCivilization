using System.Collections.ObjectModel;

namespace IncrementalCivilization.Services;
public interface ISettingsService
{
    ObservableCollection<Option> Options { get; }

    Option Get(OptionType type);
}