using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class PageViewModelBase : ObservableObject, IPageViewModel
{
    public bool Initialized { get; set; } = false;
    public string Title { get; set; }
    public SymbolRegular Icon { get; set; }

    public PageViewModelBase(string title, SymbolRegular icon)
    {
        Title = title;
        Icon = icon;
    }

    public virtual void Initialize()
    {
        Initialized = true;
    }
}
