using IncrementalCivilization.Domain;
using System.Windows;
using System.Windows.Controls;

namespace IncrementalCivilization.Mvvm.Controls;

public partial class ResourcesViewer : UserControl
{
    public IEnumerable<Resource> ItemsSource
    {
        get { return (IEnumerable<Resource>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable<Resource>), typeof(ResourcesViewer), new PropertyMetadata(null));

    public ResourcesViewer()
    {
        InitializeComponent();
    }
}
