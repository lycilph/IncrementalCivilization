using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Domain.Core;
using NLog;
using System.ComponentModel;
using System.Diagnostics;

namespace IncrementalCivilization.Domain;

public enum ResourceType { Population, Food, Wood, Minerals, Science }

[DebuggerDisplay("Name = {Name}, Value = {Value}, Maximum = {Maximum}, Active = {Active}")]
public partial class Resource : ObservableObject, ITypedItem<ResourceType>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

    [ObservableProperty]
    private bool showAsInterger = false;

    public Resource(ResourceType type, string name, double max)
    {
        Type = type;
        Name = name;
        Maximum = max;

        PropertyChanged += ResourcePropertyChanged;
    }

    public Resource(ResourceType type, double max) : this(type, type.ToString(), max) { }

    public Resource(ResourceType type) : this(type, type.ToString(), 0.0) { }

    private void ResourcePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (Value > 0)
        {
            PropertyChanged -= ResourcePropertyChanged;
            Active = true;
            logger.Debug("Resource {name} actived {property}", Name, e.PropertyName);
        }
    }

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
