using System.Numerics;

namespace Sandbox.Utils;

public class CircularBuffer<T> : List<T> where T : INumber<T>
{
    public int index = 0;

    public CircularBuffer(int capacity) : base(capacity)
    {
        for (int i = 0; i < capacity; i++)
            Add(default!);
    }

    public void Set(T item)
    {
        this[index++] = item;

        if (index >= Capacity)
            index = 0;
    }
}
