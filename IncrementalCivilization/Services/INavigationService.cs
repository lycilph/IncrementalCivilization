namespace IncrementalCivilization.Services;

public interface INavigationService
{
    void NavigateToScreen<IVM>() where IVM : notnull;
}
