namespace IncrementalCivilization.ViewModels;

public interface IViewModel
{
    public bool Initialized { get; }

    void Initialize();
}
