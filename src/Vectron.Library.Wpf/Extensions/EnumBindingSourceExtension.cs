using System.Windows.Markup;

namespace Vectron.Library.Wpf.Extensions;

/// <summary>
/// <see cref="MarkupExtension"/> that creates a bind able list of enum values. http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/.
/// </summary>
public class EnumBindingSourceExtension : MarkupExtension
{
    private Type? enumType;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumBindingSourceExtension"/> class.
    /// </summary>
    public EnumBindingSourceExtension()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumBindingSourceExtension"/> class.
    /// </summary>
    /// <param name="enumType">The <see cref="Enum"/> that needs to be bound.</param>
    public EnumBindingSourceExtension(Type enumType)
        => EnumType = enumType;

    /// <summary>
    /// Gets or sets the <see cref="Enum"/> that needs to be bound.
    /// </summary>
    public Type? EnumType
    {
        get => enumType;

        set
        {
            if (value != enumType)
            {
                if (value != null)
                {
                    var underlyingType = Nullable.GetUnderlyingType(value) ?? value;

                    if (!underlyingType.IsEnum)
                    {
                        throw new InvalidOperationException("Type must be for an Enum.");
                    }
                }

                enumType = value;
            }
        }
    }

    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (enumType == null)
        {
            throw new InvalidOperationException("The EnumType must be specified.");
        }

        var actualEnumType = Nullable.GetUnderlyingType(enumType) ?? enumType;
        var enumValues = Enum.GetValues(actualEnumType);

        if (actualEnumType == enumType)
        {
            return enumValues;
        }

        var tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
        enumValues.CopyTo(tempArray, 1);
        return tempArray;
    }
}
