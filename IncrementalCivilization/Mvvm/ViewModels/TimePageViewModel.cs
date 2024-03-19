using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace IncrementalCivilization.Mvvm.ViewModels;

public partial class TimePageViewModel : ObservableObject, ITimePageViewModel
{
    public string Title { get; set; }
    public SymbolRegular Icon { get; set; }

    public TimePageViewModel()
    {
        Title = "Time";
        Icon = SymbolRegular.HourglassHalf24;
    }
}
