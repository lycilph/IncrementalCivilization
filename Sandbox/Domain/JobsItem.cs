using CommunityToolkit.Mvvm.ComponentModel;

namespace Sandbox.Domain;

public enum JobItemType { Farmer, WoodCutter, Miner };

public class JobsBundle : ItemsBundle<JobItemType, JobItem> { }

public partial class JobItem : ObservableObject, ITypedItem<JobItemType>
{
    public JobItemType Type { get; private set; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private double count = 0;

    public JobItem(JobItemType type, string name)
    {
        Type = type;
        this.name = name;
    }

    public JobItem(JobItemType type) : this(type, type.ToString()) { }
}
