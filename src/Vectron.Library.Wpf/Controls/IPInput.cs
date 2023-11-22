using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Vectron.Library.Wpf.Controls;

/// <summary>
/// An Ip address input control.
/// </summary>
[TemplatePart(Name = ElementFirstIPPartTextBox, Type = typeof(TextBox))]
[TemplatePart(Name = ElementSecondIPPartTextBox, Type = typeof(TextBox))]
[TemplatePart(Name = ElementThirdIPPartTextBox, Type = typeof(TextBox))]
[TemplatePart(Name = ElementFourthIPPartTextBox, Type = typeof(TextBox))]
public class IpInput : Control
{
    /// <summary>
    /// Identifies the <see cref="IPAddress"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IPAddressProperty = DependencyProperty
        .Register(nameof(IPAddress), typeof(string), typeof(IpInput), new FrameworkPropertyMetadata("127.0.0.1", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IpAddressChanged));

    private const string ElementFirstIPPartTextBox = "PART_FirstIPPartTextBox";
    private const string ElementFourthIPPartTextBox = "PART_FourthIPPartTextBox";
    private const string ElementSecondIPPartTextBox = "PART_SecondIPPartTextBox";
    private const string ElementThirdIPPartTextBox = "PART_ThirdIPPartTextBox";
    private TextBox? firstIPPartTextBox;
    private TextBox? fourthIPPartTextBox;
    private TextBox? secondIPPartTextBox;
    private TextBox? thirdIPPartTextBox;

    static IpInput()
        => DefaultStyleKeyProperty.OverrideMetadata(typeof(IpInput), new FrameworkPropertyMetadata(typeof(IpInput)));

    /// <summary>
    /// Gets or sets the Ip address.
    /// </summary>
    public string IPAddress
    {
        get => (string)GetValue(IPAddressProperty);
        set => SetValue(IPAddressProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        firstIPPartTextBox = GetTemplateChild(ElementFirstIPPartTextBox) as TextBox;
        secondIPPartTextBox = GetTemplateChild(ElementSecondIPPartTextBox) as TextBox;
        thirdIPPartTextBox = GetTemplateChild(ElementThirdIPPartTextBox) as TextBox;
        fourthIPPartTextBox = GetTemplateChild(ElementFourthIPPartTextBox) as TextBox;
        UpdateTextBoxes();
    }

    /// <inheritdoc/>
    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);
        ArgumentNullException.ThrowIfNull(e);

        var frameworkElement = e.OriginalSource as FrameworkElement;

        if (frameworkElement is not TextBox textBox)
        {
            return;
        }

        if (textBox.Text.Length == 3
            && ((e.Key >= Key.D0 && e.Key <= Key.D9)
                || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
        {
            e.Handled = true;
            _ = frameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        if (firstIPPartTextBox == null || secondIPPartTextBox == null || thirdIPPartTextBox == null || fourthIPPartTextBox == null)
        {
            return;
        }

        IPAddress = $"{firstIPPartTextBox.Text}.{secondIPPartTextBox.Text}.{thirdIPPartTextBox.Text}.{fourthIPPartTextBox.Text}";
    }

    /// <inheritdoc/>
    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);
        ArgumentNullException.ThrowIfNull(e);

        var frameworkElement = e.OriginalSource as FrameworkElement;

        if (frameworkElement is not TextBox textBox)
        {
            return;
        }

        if (e.Key == Key.Decimal)
        {
            e.Handled = true;
            _ = frameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
        else if (e.Key == Key.Back && textBox.Text.Length == 0)
        {
            e.Handled = true;
            _ = frameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
        }
    }

    private static void IpAddressChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is IpInput ipInput)
        {
            ipInput.UpdateTextBoxes();
        }
    }

    private void UpdateTextBoxes()
    {
        if (firstIPPartTextBox == null || secondIPPartTextBox == null || thirdIPPartTextBox == null || fourthIPPartTextBox == null)
        {
            return;
        }

        var parts = IPAddress.Split('.');
        firstIPPartTextBox.Text = parts.Length >= 0 ? parts[0] : "0";
        secondIPPartTextBox.Text = parts.Length >= 1 ? parts[1] : "0";
        thirdIPPartTextBox.Text = parts.Length >= 2 ? parts[2] : "0";
        fourthIPPartTextBox.Text = parts.Length >= 3 ? parts[3] : "0";
    }
}
