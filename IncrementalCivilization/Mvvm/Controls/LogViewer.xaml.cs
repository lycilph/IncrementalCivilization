using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace IncrementalCivilization.Mvvm.Controls;

public partial class LogViewer : UserControl
{
    public bool ScrollOnChange
    {
        get { return (bool)GetValue(ScrollOnChangeProperty); }
        set { SetValue(ScrollOnChangeProperty, value); }
    }
    public static readonly DependencyProperty ScrollOnChangeProperty =
        DependencyProperty.Register("ScrollOnChange", typeof(bool), typeof(LogViewer), new PropertyMetadata(true));

    public ObservableCollection<string> ItemsSource
    {
        get { return (ObservableCollection<string>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<string>), typeof(LogViewer), new PropertyMetadata(OnItemsSourceChanged));

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var logViewer = (LogViewer)d;

        if (e.OldValue != null && e.OldValue is ObservableCollection<string> o)
            o.CollectionChanged -= logViewer.OnLogCollectionChanged;

        if (e.NewValue != null && e.NewValue is ObservableCollection<string> n)
            n.CollectionChanged += logViewer.OnLogCollectionChanged;
    }

    private void OnLogCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (ScrollOnChange)
            log_scrollviewer.ScrollToBottom();
    }

    private void ClearLogButtonClick(object sender, RoutedEventArgs e)
    {
        if (ItemsSource != null)
            ItemsSource.Clear();
    }

    public LogViewer()
    {
        InitializeComponent();
    }
}
