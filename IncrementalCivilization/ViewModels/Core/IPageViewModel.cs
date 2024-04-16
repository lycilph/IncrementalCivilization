using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Core;

public interface IPageViewModel : IViewModel
{
    public string Title { get; }
    public SymbolRegular Icon { get; }
    public IRelayCommand NavigateToPageCommand { get; }
}
