using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace IncrementalCivilization.Mvvm.Controls;

public partial class BuildingsViewer : UserControl
{
    public ObservableCollection<Building> BuildingsSource
    {
        get { return (ObservableCollection<Building>)GetValue(BuildingsSourceProperty); }
        set { SetValue(BuildingsSourceProperty, value); }
    }
    public static readonly DependencyProperty BuildingsSourceProperty =
        DependencyProperty.Register("BuildingsSource", typeof(ObservableCollection<Building>), typeof(BuildingsViewer), new PropertyMetadata(null));

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
