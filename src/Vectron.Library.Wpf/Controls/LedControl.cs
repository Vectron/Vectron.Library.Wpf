using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vectron.Library.Wpf.Controls;

/// <summary>
/// An control to display a led.
/// </summary>
public class LedControl : CheckBox
{
    /// <summary>
    /// The dependency property for the <see cref="OffColor"/>.
    /// </summary>
    public static readonly DependencyProperty OffColorProperty =
        DependencyProperty.Register("OffColor", typeof(Brush), typeof(LedControl), new PropertyMetadata(Brushes.Red));

    /// <summary>
    /// The dependency property for the <see cref="OnColor"/>.
    /// </summary>
    public static readonly DependencyProperty OnColorProperty =
        DependencyProperty.Register("OnColor", typeof(Brush), typeof(LedControl), new PropertyMetadata(Brushes.Green));

    static LedControl()
        => DefaultStyleKeyProperty.OverrideMetadata(typeof(LedControl), new FrameworkPropertyMetadata(typeof(LedControl)));

    /// <summary>
    /// Gets or sets the off color of the led.
    /// </summary>
    public Brush OffColor
    {
        get => (Brush)GetValue(OffColorProperty);
        set => SetValue(OffColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the on color of the led.
    /// </summary>
    public Brush OnColor
    {
        get => (Brush)GetValue(OnColorProperty);
        set => SetValue(OnColorProperty, value);
    }
}
