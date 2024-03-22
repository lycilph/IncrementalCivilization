using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;

namespace IncrementalCivilization.Domain;

public enum BuildingType { Field, Mine };

public partial class Building : ObservableObject
{
    public BuildingType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;
 
    public ResourceBundle Cost { get; set; } = [];
    public double CostIncrease { get; set; } = 1.12;

    [ObservableProperty]
    private int amount = 0;

    [ObservableProperty]
    private bool _active = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyCommand))]
    private bool canAfford = false;

    public Building(BuildingType type, ResourceBundle cost)
    {
        Type = type;
        Name = type.ToString() ?? string.Empty;
        Cost = cost;
    }

    public void Update(ResourceBundle resources)
    {
        foreach (var resource in Cost)
            resource.Amount = resources[resource.Type].Amount;

        CanAfford = Cost.GetAll().All(r => r.OverThreshold);
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

            foreach (var r in Cost.GetAll())
                r.Threshold *= CostIncrease;
        }
    }
}

public class BuildingsBundle : IEnumerable<Building>
{
    private readonly Dictionary<BuildingType, Building> buildings = [];
    
    public void Add(Building building) => buildings.Add(building.Type, building);

    public IEnumerable<Building> GetAll() => buildings.Values;

    public Building this[BuildingType type] => buildings[type];

    public IEnumerator<Building> GetEnumerator()
    {
        foreach (var b in buildings.Values)
            yield return b;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public static BuildingsBundle AllBuildings()
    {
        return
        [
            new Building(BuildingType.Field, ResourceBundle.Single(ResourceType.Food, 0, 10)),
            new Building(BuildingType.Mine, ResourceBundle.Single(ResourceType.Wood, 0, 10)
                                                          .Add(ResourceType.Mineral, 0, 5))
        ];
    }
}
