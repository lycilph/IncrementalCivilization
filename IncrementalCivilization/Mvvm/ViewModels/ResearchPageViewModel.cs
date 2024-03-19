using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class ResearchPageViewModel : ObservableObject, IResearchPageViewModel
{
    public string Title { get; set; }
    public SymbolRegular Icon { get; set; }

    public ResearchPageViewModel()
    {
        Title = "Research";
        Icon = SymbolRegular.Beaker24;
    }
}
