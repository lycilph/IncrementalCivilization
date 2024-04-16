namespace IncrementalCivilization.ViewModels.Core;

public interface IViewModel
{
    public bool Initialized { get; }
    public bool Enabled { get; }

    void Initialize();
    void Activate();
    void Deactivate();
}
