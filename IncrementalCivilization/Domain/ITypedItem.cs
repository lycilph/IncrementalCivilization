namespace IncrementalCivilization.Domain;

public interface ITypedItem<T>
{
    T Type { get; }
}
