using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public interface IPageViewModel : IViewModel
{
    public string Title { get; protected set; }
    public SymbolRegular Icon { get; protected set; }
}
