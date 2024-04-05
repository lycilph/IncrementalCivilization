using CommunityToolkit.Mvvm.ComponentModel;

namespace IncrementalCivilization.Domain;

public enum JobItemType { Farmer, WoodCutter, Scholar };

public partial class JobItem : ObservableObject, ITypedItem<JobItemType>
{
    public JobItemType Type { get; private set; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int count = 0;

    [ObservableProperty]
    private bool active = false;

    public JobItem(JobItemType type, string name)
    {
        Type = type;
        Name = name;
    }

    public JobItem(JobItemType type) : this(type, type.ToString()) { }
}
