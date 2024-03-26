namespace Sandbox.Domain;

public static class JobsExtensions
{
    public static JobsBundle AllJobs()
    {
        var bundle = new JobsBundle();

        var farmer = new JobItem(JobItemType.Farmer);
        bundle.Add(farmer);

        var woodCutter = new JobItem(JobItemType.WoodCutter, "Wood Cutter");
        bundle.Add(woodCutter);

        return bundle;
    }

    public static JobItem Farmer(this JobsBundle bundle)
    {
        return bundle[JobItemType.Farmer];
    }
}
