namespace IncrementalCivilization.Utils;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Apply<T>(this IEnumerable<T> e, Action<T> action)
    {
        foreach (var item in e)
            action(item);
        return e;
    }
}
