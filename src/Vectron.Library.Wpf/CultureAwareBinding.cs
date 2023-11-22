using System.Globalization;
using System.Windows.Data;

namespace Vectron.Library.Wpf;

/// <summary>
/// A <see cref="Binding"/> that sets the converter culture to the apps current.
/// </summary>
public class CultureAwareBinding : Binding
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CultureAwareBinding"/> class.
    /// </summary>
    public CultureAwareBinding()
        : base()
        => ConverterCulture = CultureInfo.CurrentCulture;

    /// <summary>
    /// Initializes a new instance of the <see cref="CultureAwareBinding"/> class.
    /// </summary>
    /// <param name="path">The initial System.Windows.Data.Binding.Path for the binding.</param>
    public CultureAwareBinding(string path)
        : base(path)
        => ConverterCulture = CultureInfo.CurrentCulture;
}
