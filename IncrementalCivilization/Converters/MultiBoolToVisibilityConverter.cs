using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IncrementalCivilization.Converters;

public class MultiBoolToVisibilityConverter : IMultiValueConverter
{
    public bool Invert { get; set; }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        foreach (var v in values)
            if (v is not bool)
                return Visibility.Collapsed;

        var all_true = values.All(v => (bool)v == true);

        if (Invert)
            all_true = !all_true;

        return all_true ? Visibility.Visible : Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
