namespace IncrementalCivilization.Domain;

public static class JobsExtensions
{
    public static JobsBundle AllJobs()
    {
        return
        [
            new(JobItemType.Farmer),
            new(JobItemType.WoodCutter, "Wood Cutter"),
            new(JobItemType.Scholar)
        ];
    }

    public static JobItem Farmer(this JobsBundle bundle)
    {
        return bundle[JobItemType.Farmer];
    }

    public static JobItem WoodCutters(this JobsBundle bundle)
    {
        return bundle[JobItemType.WoodCutter];
    }

    public static JobItem Scholar(this JobsBundle bundle)
    {
        return bundle[JobItemType.Scholar];
    }
}
