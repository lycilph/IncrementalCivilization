using IncrementalCivilization.ViewModels.Core;

namespace IncrementalCivilization.ViewModels.Screens;

public interface IMainScreenViewModel : IViewModel
{
    public IPageViewModel? CurrentPage { get; set; }
}