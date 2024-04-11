using IncrementalCivilization.Utils;

namespace IncrementalCivilization.Domain;

public class CostsBundle : Bundle<ResourceType, Cost>
{
    public void SubtractCostsFromResources()
    {
        this.Apply(i => i.SubtractCostFromResource());
    }

    public void Mult(double v)
    {
        this.Apply(i => i.CostValue *= v);
    }
}