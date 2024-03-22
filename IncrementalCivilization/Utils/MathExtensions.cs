namespace IncrementalCivilization.Utils;

public static class MathExtensions
{
    public static T Clamp<T>(this T value, T min, T max) where T : notnull, IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            return min;
        if (value.CompareTo(max) > 0)
            return max;

        return value;
    }
}
