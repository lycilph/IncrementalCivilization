namespace IncrementalCivilization.Domain;

public interface IGame
{
    Time Time { get; }
    ResourceBundle Resources { get; }

    void Initialize();
}