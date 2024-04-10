using Wpf.Ui.Controls;

namespace IncrementalCivilization.ViewModels.Shared;

public partial class PageViewModelBase(string title, SymbolRegular icon) : ViewModelBase, IPageViewModel
{
    public string Title { get; protected set; } = title;
    public SymbolRegular Icon { get; protected set; } = icon;
}
