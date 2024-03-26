namespace Sandbox.Domain;

public interface ITypedItem<T>
{
    T Type { get; }
}
