﻿using CommunityToolkit.Mvvm.ComponentModel;
using Sandbox.Utils;

namespace Sandbox.Domain;

public enum ResourceItemType { Population, Food, Wood, Mineral, Iron };

public class ResourcesBundle : ItemsBundle<ResourceItemType, ResourceItem> { }

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

    public ResourceItem(ResourceItemType type, string name)
    {
        Type = type;
        this.name = name;
    }

    public ResourceItem(ResourceItemType type) : this(type, type.ToString()) { }

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

    // This does NOT update the rates (used when buying buildings)
    public void Subtract(double v) 
    {
        updateRate = false;
        Value -= v;
        updateRate = true;
    }
}