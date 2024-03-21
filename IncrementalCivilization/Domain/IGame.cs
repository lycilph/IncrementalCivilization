namespace IncrementalCivilization.Domain;

public interface IGame
{
    Time Time { get; }
    ResourceBundle Resources { get; }
    BuildingsBundle Buildings { get; }

    void Initialize();
}