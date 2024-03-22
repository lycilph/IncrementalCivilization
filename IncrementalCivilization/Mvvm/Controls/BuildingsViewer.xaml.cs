using IncrementalCivilization.Domain;
using System.Windows;
using System.Windows.Controls;

namespace IncrementalCivilization.Mvvm.Controls;

public partial class BuildingsViewer : UserControl
{
    public IEnumerable<Building> BuildingsSource
    {
        get { return (IEnumerable<Building>)GetValue(BuildingsSourceProperty); }
        set { SetValue(BuildingsSourceProperty, value); }
    }
    public static readonly DependencyProperty BuildingsSourceProperty =
        DependencyProperty.Register("BuildingsSource", typeof(IEnumerable<Building>), typeof(BuildingsViewer), new PropertyMetadata(null));

    public ResourceBundle ResourcesSource
    {
        get { return (ResourceBundle)GetValue(ResourcesSourceProperty); }
        set { SetValue(ResourcesSourceProperty, value); }
    }
    public static readonly DependencyProperty ResourcesSourceProperty =
        DependencyProperty.Register("ResourcesSource", typeof(ResourceBundle), typeof(BuildingsViewer), new PropertyMetadata(null));

    public BuildingsViewer()
    {
        InitializeComponent();
    }
}
