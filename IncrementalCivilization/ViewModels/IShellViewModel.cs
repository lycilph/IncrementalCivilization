namespace IncrementalCivilization.ViewModels;

public interface IShellViewModel : IViewModel
{
    public IViewModel? Current { get; set; }
}
