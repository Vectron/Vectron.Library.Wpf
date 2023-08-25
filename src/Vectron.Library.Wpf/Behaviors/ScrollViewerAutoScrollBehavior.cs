using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Vectron.Library.Wpf.Behaviors;

/// <summary>
/// A behavior that will scroll the scroll viewer always to the bottom.
/// </summary>
public sealed class ScrollViewerAutoScrollBehavior : Behavior<ScrollViewer>
{
    private bool autoScroll = true;

    /// <inheritdoc/>
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.ScrollChanged += ScrollViewer_ScrollChanged;
        ScrollIntoView();
    }

    /// <inheritdoc/>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.ScrollChanged -= ScrollViewer_ScrollChanged;
    }

    private void ScrollIntoView() => _ = AssociatedObject.Dispatcher.BeginInvoke(AssociatedObject.ScrollToBottom);

    private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (sender is not ScrollViewer scrollViewer
            || scrollViewer != AssociatedObject)
        {
            return;
        }

        if (e.ExtentHeightChange != 0 && autoScroll)
        {
            ScrollIntoView();
            return;
        }

        var scrollPosition = e.ExtentHeight - e.ViewportHeight;
        autoScroll = e.VerticalOffset == scrollPosition;
    }
}
