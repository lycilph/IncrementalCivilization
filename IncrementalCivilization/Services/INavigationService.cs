using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.Services;

public interface INavigationService
{
    void NavigateToScreen<IVM>() where IVM : notnull;
    void NavigateToPage<IVM>() where IVM : IPageViewModel;
    void NavigateToPage(IPageViewModel page);
}
