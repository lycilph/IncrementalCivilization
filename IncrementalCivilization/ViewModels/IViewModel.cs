namespace IncrementalCivilization.ViewModels;

public interface IViewModel
{
    public bool Initialized { get; }
    public bool Enabled { get; }

    void Initialize();
}
