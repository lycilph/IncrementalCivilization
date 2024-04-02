using IncrementalCivilization.ViewModels.Pages;

namespace IncrementalCivilization.Services;

public interface INavigationService
{
    void NavigateTo<IVM>();
    void NavigateToPage(IPageViewModel page);
}