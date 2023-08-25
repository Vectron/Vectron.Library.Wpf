using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Vectron.Library.Wpf.Behaviors;

/// <summary>
/// A <see cref="Behavior"/> that keeps the selected item in view.
/// </summary>
public class ListBoxScrollIntoViewBehavior : Behavior<ListBox>
{
    /// <inheritdoc/>
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.SelectionChanged += ListBox_SelectionChanged;
        ScrollIntoView();
    }

    /// <inheritdoc/>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.SelectionChanged -= ListBox_SelectionChanged;
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender != AssociatedObject)
        {
            return;
        }

        ScrollIntoView();
    }

    private void ScrollIntoView()
        => _ = AssociatedObject.Dispatcher.BeginInvoke(() =>
        {
            AssociatedObject.UpdateLayout();
            if (AssociatedObject.SelectedItem != null)
            {
                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
            }
        });
}
