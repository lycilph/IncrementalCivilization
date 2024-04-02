using IncrementalCivilization.ViewModels.Pages;

namespace IncrementalCivilization.ViewModels;

public interface IMainViewModel : IViewModel 
{
    IPageViewModel? CurrentPage { get; set; }
}
