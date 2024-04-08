namespace IncrementalCivilization.Domain;

public class JobsBundle : ItemsBundle<JobItemType, JobItem>
{
    public JobItem Farmer { get; private set; } = new JobItem(JobItemType.Farmer);
    public JobItem WoodCutter { get; private set; } = new JobItem(JobItemType.WoodCutter);
    public JobItem Scholar { get; private set; } = new JobItem(JobItemType.Scholar);

    public int EmployedPeople() => this.Aggregate(0, (a,b) => a + b.Count);

    public void Limit(ResourceItem population)
    {
        var to_remove = (int)(EmployedPeople() - population.Value);
        for (int i = 0; i < to_remove; i++)
        {
            var job = this.First(i => i.Count > 0);
            job.Count -= 1;
        }
    }

    public static JobsBundle AllJobs()
    {
        var bundle = new JobsBundle()
        {
            Farmer = new(JobItemType.Farmer),
            WoodCutter = new(JobItemType.WoodCutter, "Wood Cutter"),
            Scholar = new(JobItemType.Scholar)
        };

        bundle.Add(bundle.Farmer);
        bundle.Add(bundle.WoodCutter); 
        bundle.Add(bundle.Scholar);

        return bundle;
    }
}
