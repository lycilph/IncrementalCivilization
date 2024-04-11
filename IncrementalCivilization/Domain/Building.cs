using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Utils;
using System.Collections.Specialized;

namespace IncrementalCivilization.Domain;

public enum BuildingType { Field, Hut, Library, Test };

public partial class Building : ObservableObject, ITypedItem<BuildingType>
{
    public BuildingType Type { get; private set; }

    public CostsBundle Cost { get; set; } = [];
    public Action BuyAction { get; set; } = () => { };

    [ObservableProperty]
    private double costIncrease = 1.15;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private int _count = 0;

    [ObservableProperty]
    private bool _active = false;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BuyCommand))]
    private bool _canBuy = false;

    public Building(BuildingType type, string name)
    {
        Type = type;
        Name = name;

        Cost.CollectionChanged += CostCollectionChanged;
    }

    public Building(BuildingType type) : this(type, type.ToString()) { }

    private void CostCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            foreach (var i in e.NewItems)
                if (i is Cost c && c != null)
                    c.PropertyChanged += (s, e) => UpdateCanBuy();
        UpdateCanBuy();
    }

    private void UpdateCanBuy()
    {
        CanBuy = Cost.All(c => c.CanAfford);
    }

    [RelayCommand(CanExecute = nameof(CanBuy))]
    public void Buy()
    {
        Count++;
        Cost.SubtractCostsFromResources();
        Cost.Mult(CostIncrease);
        BuyAction();
    }
}
