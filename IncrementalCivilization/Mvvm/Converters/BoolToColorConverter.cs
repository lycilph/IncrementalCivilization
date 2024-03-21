using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IncrementalCivilization.Mvvm.Converters;

public class BoolToColorConverter : IValueConverter
{
    public required SolidColorBrush TrueColor { get; set; }
    public required SolidColorBrush FalseColor { get; set; }
    public bool Invert { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var v = (bool)value;
        if (Invert)
            v = !v;

        return v ? TrueColor : FalseColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
