using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IncrementalCivilization.Domain.Core;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace IncrementalCivilization.Domain;

public enum BuildingType { Field, Hut, Library, Barn, Mine, Workshop };

[DebuggerDisplay("Name = {Name}, Count = {Count}, Active = {Active}")]
public partial class Building : ObservableObject, ITypedItem<BuildingType>
{
    public BuildingType Type { get; private set; }

    public CostsBundle Cost { get; set; } = [];
    public Action BuyAction { get; set; } = () => { };
    public double ActivationThreshold { get; private set; } = 0.3;

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

        Cost.CollectionChanged += UpdateCanBuyOnCollectionChanged;
        Cost.PropertyChanged += UpdateCanBuyOnPropertyChanged;
        Cost.PropertyChanged += ActivateOnPropertyChanged;
    }


    public Building(BuildingType type) : this(type, type.ToString()) { }

    private void ActivateOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (Cost.IsOverThreshold(ActivationThreshold))
        {
            Cost.PropertyChanged -= ActivateOnPropertyChanged;
            Active = true;
        }
    }

    private void UpdateCanBuyOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        UpdateCanBuy();
    }

    private void UpdateCanBuyOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
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

    public void Clear()
    {
        Cost.CollectionChanged -= UpdateCanBuyOnCollectionChanged;
        Cost.PropertyChanged -= UpdateCanBuyOnPropertyChanged;
        Cost.PropertyChanged -= ActivateOnPropertyChanged;

        Cost.Clear();
    }
}
