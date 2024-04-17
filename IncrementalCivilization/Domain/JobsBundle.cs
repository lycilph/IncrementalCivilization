using IncrementalCivilization.Domain.Core;

namespace IncrementalCivilization.Domain;

public class JobsBundle : Bundle<JobType, Job>
{
    public Job Farmer { get; private set; }
    public Job WoodCutter { get; private set; }
    public Job Scholar { get; private set; }
    public Job Miner { get; private set; }

    public JobsBundle()
    {
        Farmer = new Job(JobType.Farmer);
        WoodCutter = new Job(JobType.WoodCutter, "Wood Cutter");
        Scholar = new Job(JobType.Scholar);
        Miner = new Job(JobType.Miner);

        Add(Farmer, WoodCutter, Scholar, Miner);
    }

    public void Limit(int max)
    {
        var employedPeople = this.Sum(j => j.Count);
        var to_remove = employedPeople - max;
        for (int i = 0; i < to_remove; i++)
        {
            var job = this.First(i => i.Count > 0);
            job.Count -= 1;
        }
    }
}
