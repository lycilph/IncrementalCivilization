using System.Numerics;

namespace IncrementalCivilization.Utils;

public class CircularBuffer<T> : List<T> where T : INumber<T>
{
    public int index = 0;

    public CircularBuffer(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
            base.Add(default!);
    }

    public void Insert(T item)
    {
        this[index++] = item;

        if (index >= Capacity)
            index = 0;
    }
}
