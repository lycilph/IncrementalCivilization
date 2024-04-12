using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class CostsBundle : Bundle<ResourceType, Cost>
{
    public void SubtractCostsFromResources()
    {
        this.Apply(i => i.SubtractCostFromResource());
    }

    public bool IsOverThreshold(double threshold)
    {
        return this.All(c => c.IsOverThreshold(threshold));
    }

    public void Mult(double v)
    {
        this.Apply(i => i.CostValue *= v);
    }
}