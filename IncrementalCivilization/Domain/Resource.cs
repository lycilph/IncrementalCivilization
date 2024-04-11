using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;
using System.Diagnostics;

namespace IncrementalCivilization.Domain;

public enum ResourceType { Population, Food, Wood, Science }

[DebuggerDisplay("Name = {Name}, Value = {Value}, Maximum = {Maximum}")]
public partial class Resource : ObservableObject, ITypedItem<ResourceType>
{
    private readonly CircularBuffer<double> rateBuffer = new(10);

    public ResourceType Type { get; private set; }

    [ObservableProperty]
    private bool active = false;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private double _value = 0;

    [ObservableProperty]
    private double _maximum = 0;

    [ObservableProperty]
    private double rate = 0;

    [ObservableProperty]
    private bool showRate = true;

    public Resource(ResourceType type, string name, double max)
    {
        Type = type;
        Name = name;
        Maximum = max;
    }

    public Resource(ResourceType type, double max) : this(type, type.ToString(), max) { }

    public Resource(ResourceType type) : this(type, type.ToString(), 0.0) { }

    public void Add(double v, bool skipRateUpdate = false)
    {
        Value += v;

        if (!skipRateUpdate)
        {
            rateBuffer.Insert(v);
            Rate = rateBuffer.Average();
        }
    }

    public void Sub(double v, bool skipRateUpdate = false)
    {
        Value -= v;

        if (!skipRateUpdate)
        {
            rateBuffer.Insert(v);
            Rate = rateBuffer.Average();
        }
    }

    public void Limit()
    {
        Value = Math.Clamp(Value, 0, Maximum);
    }
}
