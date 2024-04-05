using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public enum ResourceItemType { Population, Food, Wood, Science }

public partial class ResourceItem : ObservableObject, ITypedItem<ResourceItemType>
{
    private readonly CircularBuffer<double> rateBuffer = new(10);
    private bool updateRate = true;

    public ResourceItemType Type { get; private set; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private double value = 0;

    [ObservableProperty]
    private double maximum = 0;

    [ObservableProperty]
    private double rate = 0;

    [ObservableProperty]
    private bool showRate = true;

    [ObservableProperty]
    private bool active = false;

    public ResourceItem(ResourceItemType type, string name, double max)
    {
        Type = type;
        Name = name;
        Maximum = max;
    }

    public ResourceItem(ResourceItemType type, double max) : this(type, type.ToString(), max) { }

    public ResourceItem(ResourceItemType type) : this(type, type.ToString(), 0.0) { }

    partial void OnValueChanged(double oldValue, double newValue)
    {
        if (!updateRate)
            return;

        rateBuffer.Set(newValue - oldValue);

        var total = 0.0;
        rateBuffer.Apply(r => total += r);
        Rate = total / rateBuffer.Capacity;
    }

    public void Limit()
    {
        Value = Math.Clamp(Value, 0, Maximum);
    }

    public void Add(double v, bool skipRateUpdate = false)
    {
        updateRate = !skipRateUpdate;
        Value += v;
        updateRate = true;
    }

    // This does NOT update the rates (used when buying buildings)
    public void Subtract(double v, bool skipRateUpdate = false)
    {
        updateRate = !skipRateUpdate;
        Value -= v;
        updateRate = true;
    }
}
