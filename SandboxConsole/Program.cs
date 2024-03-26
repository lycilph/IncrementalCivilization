using System.Numerics;

namespace SandboxConsole;
public static class EnumerableExtensions
{
    public static IEnumerable<T> Apply<T>(this IEnumerable<T> e, Action<T> action)
    {
        foreach (var item in e)
            action(item);
        return e;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        CircularBuffer<double> rates = new CircularBuffer<double>(5);

        rates.Set(0.1);
        rates.Set(0.2);
        rates.Set(0.3);
        rates.Set(0.4);
        rates.Set(0.5);
        rates.Set(0.6);
        rates.Set(0.7);
        rates.Set(0.8);
        rates.Set(0.9);
        rates.Set(1.0);
        rates.Set(1.1);

        foreach (var rate in rates)
            Console.WriteLine(rate);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
