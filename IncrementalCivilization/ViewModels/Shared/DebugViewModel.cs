using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain;
using IncrementalCivilization.Utils;
using IncrementalCivilization.ViewModels.Core;
using System.Collections.ObjectModel;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class DebugViewModel(Game game) : ViewModelBase
{
    public ObservableCollection<ProgessEvent> Events { get => game.ProgressEventManager.Events; }

    [RelayCommand]
    private void EnableAllPages()
    {
        game.Capabilities.ResearchPageEnabled = true;
        game.Capabilities.UpgradesPageEnabled = true;
        game.Capabilities.TimePageEnabled = true;
    }

    [RelayCommand]
    private void EnableRefineFood()
    {
        game.Capabilities.RefineFoodEnabled = true;
        game.Capabilities.RefineAllEnabled = true;
    }

    [RelayCommand]
    private void EnableAllResources()
    {
        game.Resources.Apply(r => r.Active = true);
    }

    [RelayCommand]
    private void EnableAllJobs()
    {
        game.Jobs.Apply(j => j.Active = true);
    }

    [RelayCommand]
    private void EnableAllBuildings()
    {
        game.Buildings.Apply(b  => b.Active = true);
    }

    [RelayCommand]
    private void ClearResources()
    {
        game.Resources.Apply(r => r.Value = 0);
    }

    [RelayCommand]
    private void MaxAllResources()
    {
        AddFood("0");
        AddWood("0");
        AddMinerals("0");
        AddScience("0");
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


    [RelayCommand]
    private void ClearBuildings()
    {
        game.Buildings.Apply(r => r.Count = 0);
    }

    [RelayCommand]
    private void AddFields(string amount)
    {
        if (int.TryParse(amount, out int value))
            for (int i = 0; i < value; i++)
                game.Buildings.Field.Buy();
    }

    [RelayCommand]
    private void AddHuts(string amount)
    {
        if (int.TryParse(amount, out int value))
            for (int i = 0; i < value; i++)
                game.Buildings.Hut.Buy();
    }

    [RelayCommand]
    private void AddLibraries(string amount)
    {
        if (int.TryParse(amount, out int value))
            for (int i = 0; i < value; i++)
                game.Buildings.Library.Buy();
    }
}