using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;

namespace IncrementalCivilization.Domain;

public enum JobType { Farmer, WoodCutter, Miner };

public partial class Job : ObservableObject
{
    public ResourceType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;

    [ObservableProperty]
    private double _amount = 0;

    [ObservableProperty]
    private bool _active = false;

    public Job(JobType type)
    {
        Type = type;
        Name = type.ToString() ?? string.Empty;
    }
}

public class JobsBundle : IEnumerable<Job>
{
    private readonly Dictionary<JobType, Job> Jobs = [];

    public Dictionary<JobType, Job>.KeyCollection Keys { get => Jobs.Keys; }

    public JobsBundle Add(JobType type)
    {
        Jobs.Add(type, new Job(type));
        return this;
    }

    public Job this[JobType type] => Jobs[type];

    public IEnumerator<Job> GetEnumerator()
    {
        foreach (var j in Jobs.Values)
            yield return j;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Job>)this).GetEnumerator();

    public static JobsBundle AllJobs()
    {
        var bundle = new JobsBundle();
        foreach (var j in Enum.GetValues(typeof(JobType)).Cast<JobType>())
            bundle.Add(j);
        return bundle;
    }
}