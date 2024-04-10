using IncrementalCivilization.ViewModels.Shared;

namespace IncrementalCivilization.Services;

public interface INavigationService
{
    void NavigateToScreen<IVM>() where IVM : notnull;
    void NavigateToPage(IPageViewModel page);
}
