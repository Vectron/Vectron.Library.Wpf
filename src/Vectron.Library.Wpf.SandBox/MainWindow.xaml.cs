using System.Windows;
using Vectron.Library.Wpf.Dialogs;

namespace Vectron.Library.Wpf.SandBox;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
        => InitializeComponent();

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var fontDialog = new FontPickerDialog();
        fontDialog.ShowDialog();
    }
}
