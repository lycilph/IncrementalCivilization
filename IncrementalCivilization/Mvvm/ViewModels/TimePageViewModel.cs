using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class TimePageViewModel : PageViewModelBase, ITimePageViewModel
{
    public TimePageViewModel() : base("Time", SymbolRegular.HourglassHalf24)
    {}
}
