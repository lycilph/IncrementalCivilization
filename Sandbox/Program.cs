using IncrementalCivilization.Domain;

namespace Sandbox;

internal class Program
{
    static void Main(string[] _)
    {
        var resources = new ResourceBundle();
        var buildings = new BuildingsBundle(resources);

        resources.Food.Add(5);

        buildings.Clear();

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }
}
