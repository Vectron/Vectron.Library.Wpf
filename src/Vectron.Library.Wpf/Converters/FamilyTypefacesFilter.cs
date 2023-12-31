using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Vectron.Library.Wpf.Converters;

/// <summary>
/// Provides a type converter to convert <see cref="FamilyTypefaceCollection"/> objects to and from
/// various other representations.
/// </summary>
[ValueConversion(typeof(FamilyTypefaceCollection), typeof(object))]
internal sealed class FamilyTypefacesFilter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var propName = (string)parameter;
        var items = (FamilyTypefaceCollection)value;
        var propInfo = typeof(FamilyTypeface).GetProperty(propName);
        if (propInfo == null)
        {
            return null;
        }

        return items.Select(propInfo.GetValue).Distinct();
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException("Can't convert back");
}
