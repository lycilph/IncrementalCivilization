using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;

namespace IncrementalCivilization.Domain;

public enum BuildingType { Field, Hut, Mine };

public partial class Building : ObservableObject
{
    public BuildingType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ResourceBundle Cost { get; set; } = [];
    public double CostIncrease { get; set; } = 1.15;
    public Action BuyAction { get; set; } = () => { };

    [ObservableProperty]
    private int amount = 0;

    [ObservableProperty]
    private bool _active = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyCommand))]
    private bool canAfford = false;

    public Building(BuildingType type, ResourceBundle cost, double increase = 1.15)
    {
        Type = type;
        Name = type.ToString() ?? string.Empty;
        Cost = cost;
        CostIncrease = increase;
    }

    public void Update(ResourceBundle resources)
    {
        foreach (var resource in Cost)
            resource.Amount = resources[resource.Type].Amount;

        CanAfford = Cost.All(r => r.OverThreshold);
    }

    [RelayCommand(CanExecute = nameof(CanAfford))]
    private void Buy(ResourceBundle resources)
    {
        Update(resources);
        if (CanAfford)
        {
            Amount += 1;

            foreach (var key in Cost.Keys.Intersect(resources.Keys))
                resources[key].Amount -= Cost[key].Threshold;

            foreach (var r in Cost)
                r.Threshold *= CostIncrease;

            BuyAction();
        }
    }

    public Building AddBuyAction(Action buyAction)
    {
        BuyAction = buyAction;
        return this;
    }
}

public class BuildingsBundle : IEnumerable<Building>
{
    private readonly Dictionary<BuildingType, Building> buildings = [];

    public BuildingsBundle Add(Building building)
    {
        buildings.Add(building.Type, building);
        return this;
    }

    public Building this[BuildingType type] => buildings[type];

    public IEnumerator<Building> GetEnumerator()
    {
        foreach (var b in buildings.Values)
            yield return b;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Building>)this).GetEnumerator();
}
