namespace SandboxConsole;

public class CircularBuffer<T> : List<T>
{
    public int index = 0;

    public CircularBuffer(int capacity) : base(capacity) { }

    public void Set(T item)
    {
        if (Count < Capacity)
            Add(item);
        else
            this[index++] = item;

        if (index >= Capacity)
            index = 0;
    }
}
