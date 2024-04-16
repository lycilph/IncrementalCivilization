using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Utils;
using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class DebugViewModel(Game game) : ViewModelBase
{

    [RelayCommand]
    private void EnableAllResources()
    {
        game.Resources.Apply(r => r.Active = true);
    }

    [RelayCommand]
    private void EnableAllPages()
    {
        game.Capabilities.ResearchPageEnabled = true;
        game.Capabilities.UpgradesPageEnabled = true;
        game.Capabilities.TimePageEnabled = true;
    }

    [RelayCommand]
    private void ClearResources()
    {
        game.Resources.Apply(r => r.Value = 0);
    }
    
    [RelayCommand]
    private void AddPopulation()
    {
        game.Resources.Population.Add(1, skipRateUpdate: true);
    }

    [RelayCommand]
    private void RemovePopulation()
    {
        game.Resources.Population.Sub(1, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddPopulationMax()
    {
        game.Resources.Population.Maximum += 1;
    }

    [RelayCommand]
    private void AddFood(string amount)
    {
        if (int.TryParse(amount, out int value))
            game.Resources.Food.Add(value == 0 ? game.Resources.Food.Maximum : value, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddWood(string amount)
    {
        if (int.TryParse(amount, out int value))
            game.Resources.Wood.Add(value == 0 ? game.Resources.Wood.Maximum : value, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddMinerals(string amount)
    {
        if (int.TryParse(amount, out int value))
            game.Resources.Minerals.Add(value == 0 ? game.Resources.Minerals.Maximum : value, skipRateUpdate: true);
    }

    [RelayCommand]
    private void AddScience(string amount)
    {
        if (int.TryParse(amount, out int value))
            game.Resources.Science.Add(value == 0 ? game.Resources.Science.Maximum : value, skipRateUpdate: true);
    }
}
