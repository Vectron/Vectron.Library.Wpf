using System.ComponentModel;

namespace Vectron.Library.Wpf.Converters;

/// <summary>
/// Provides a type converter to convert <see cref="Enum"/> objects to and from various other
/// representations. http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EnumDescriptionTypeConverter"/> class.
/// </remarks>
/// <param name="type">The type to convert.</param>
public class EnumDescriptionTypeConverter(Type type) : EnumConverter(type)
{
    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType != typeof(string) || value == null)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        var fi = value.GetType().GetField(value.ToString()!);
        if (fi == null)
        {
            return string.Empty;
        }

        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
        return attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Description)
            ? attributes[0].Description
            : value.ToString();
    }
}
