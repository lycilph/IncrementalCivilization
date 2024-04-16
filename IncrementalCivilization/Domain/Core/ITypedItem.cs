namespace IncrementalCivilization.Domain.Core;

public interface ITypedItem<T>
{
    T Type { get; }
}
