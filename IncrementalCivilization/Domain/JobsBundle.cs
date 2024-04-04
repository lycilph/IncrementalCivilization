namespace IncrementalCivilization.Domain;

public class JobsBundle : ItemsBundle<JobItemType, JobItem>
{
    public JobItem Farmer { get; private set; } = new JobItem(JobItemType.Farmer);
    public JobItem WoodCutter { get; private set; } = new JobItem(JobItemType.WoodCutter);
    public JobItem Scholar { get; private set; } = new JobItem(JobItemType.Scholar);

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
