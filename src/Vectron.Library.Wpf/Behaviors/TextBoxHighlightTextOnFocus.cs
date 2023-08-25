using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace Vectron.Library.Wpf.Behaviors;

/// <summary>
/// Attached properties for <see cref="TextBox"/>.
/// </summary>
public class TextBoxHighlightTextOnFocus : Behavior<TextBox>
{
    /// <inheritdoc/>
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.GotKeyboardFocus += OnKeyboardFocusSelectText;
        AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDownSetFocus;
    }

    /// <inheritdoc/>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.GotKeyboardFocus -= OnKeyboardFocusSelectText;
        AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDownSetFocus;
    }

    private static T? FindAncestor<T>(DependencyObject current)
        where T : DependencyObject
    {
        current = VisualTreeHelper.GetParent(current);

        while (current != null)
        {
            if (current is T t)
            {
                return t;
            }

            current = VisualTreeHelper.GetParent(current);
        }

        return null;
    }

    private static void OnKeyboardFocusSelectText(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (e.OriginalSource is TextBox textBox)
        {
            textBox.SelectAll();
        }
    }

    private static void OnMouseLeftButtonDownSetFocus(object sender, MouseButtonEventArgs e)
    {
        var textBox = FindAncestor<TextBox>((DependencyObject)e.OriginalSource);

        if (textBox == null)
        {
            return;
        }

        if (!textBox.IsKeyboardFocusWithin)
        {
            _ = textBox.Focus();
            e.Handled = true;
        }
    }
}
