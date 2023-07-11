using System.Globalization;
using System.Windows.Data;

namespace Vectron.Library.Wpf.Converters;

/// <summary>
/// Provides a type converter to convert <see cref="string"/> objects to and from number types.
/// </summary>
[ValueConversion(typeof(string), typeof(object))]
public class StringToInt : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value;

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string items)
        {
            return 0;
        }

        if (targetType == typeof(short))
        {
            return short.Parse(items, culture);
        }

        if (targetType == typeof(int))
        {
            return int.Parse(items, culture);
        }

        if (targetType == typeof(long))
        {
            return long.Parse(items, culture);
        }

        if (targetType == typeof(float))
        {
            return float.Parse(items, culture);
        }

        if (targetType == typeof(double))
        {
            return double.Parse(items, culture);
        }

        return 0;
    }
}
