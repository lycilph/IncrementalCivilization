using IncrementalCivilization.ViewModels.Pages;

namespace IncrementalCivilization.Services;

public interface INavigationService
{
    void NavigateToMain();
    void NavigateToSettings();
    void NavigateTo<IVM>();
    void NavigateToPage(IPageViewModel page);
}