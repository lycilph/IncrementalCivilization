using IncrementalCivilization.Domain;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace IncrementalCivilization.Mvvm.Controls;

public partial class ResourcesViewer : UserControl
{
    public ObservableCollection<Resource> ItemsSource
    {
        get { return (ObservableCollection<Resource>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<Resource>), typeof(ResourcesViewer), new PropertyMetadata(null));

    public ResourcesViewer()
    {
        InitializeComponent();
    }
}
