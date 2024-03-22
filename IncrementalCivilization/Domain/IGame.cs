namespace IncrementalCivilization.Domain;

public interface IGame
{
    Time Time { get; }
    Statistics Statistics { get; }
    ResourceBundle Resources { get; }
    BuildingsBundle Buildings { get; }

    void Initialize();
    void Cleanup();
}