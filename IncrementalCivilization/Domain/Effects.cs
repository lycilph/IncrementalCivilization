using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;

public partial class Effects : ObservableObject
{
    [ObservableProperty]
    private bool _refineFoodEnabled = false;

    [ObservableProperty]
    private bool _researchPageEnabled = false;

    [ObservableProperty]
    private bool _timePageEnabled = false;

    public double FarmerEffieciency { get; set; } = 1.0;
    public double WoodCutterEffieciency { get; set; } = 1.0;
    public double ScholarEffieciency { get; set; } = 1.0;
}
