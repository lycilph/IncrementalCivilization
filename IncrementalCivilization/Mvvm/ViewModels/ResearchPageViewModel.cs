using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class ResearchPageViewModel : PageViewModelBase, IResearchPageViewModel
{
    public ResearchPageViewModel() : base("Research", SymbolRegular.Beaker24)
    {}
}
