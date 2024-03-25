namespace IncrementalCivilization.Domain;

public interface IGame
{
    Time Time { get; }
    Statistics Statistics { get; }
    ResourceBundle Resources { get; }
    BuildingsBundle Buildings { get; }
    JobsBundle Jobs { get; }

    void Initialize();
    void Cleanup();
}