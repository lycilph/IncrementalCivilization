using CommunityToolkit.Mvvm.ComponentModel;
using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public enum JobType { Farmer, WoodCutter, Scholar, Miner };

public partial class Job : ObservableObject, ITypedItem<JobType>
{
    public JobType Type { get; private set; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int count = 0;

    [ObservableProperty]
    private bool active = false;

    public Job(JobType type, string name)
    {
        Type = type;
        Name = name;
    }

    public Job(JobType type) : this(type, type.ToString()) { }
}
