using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class JobsBundle : Bundle<JobType, Job>
{
    public Job Farmer { get; private set; }
    public Job WoodCutter { get; private set; }
    public Job Scholar { get; private set; }

    public JobsBundle()
    {
        Farmer = new Job(JobType.Farmer) { Active = true };
        WoodCutter = new Job(JobType.WoodCutter, "Wood Cutter") { Active = true };
        Scholar = new Job(JobType.Scholar) { Active = true };

        Add(Farmer, WoodCutter, Scholar);
    }
}
