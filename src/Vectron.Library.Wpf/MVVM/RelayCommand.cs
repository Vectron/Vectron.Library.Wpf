using System.Windows.Input;

namespace Vectron.Library.Wpf.MVVM;

/// <summary>
/// A <see cref="ICommand"/> to bind to commands.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RelayCommand"/> class.
/// </remarks>
/// <param name="execute">The Action that needs to be executed when command is triggered.</param>
/// <param name="canExecute">The action to check if the command can be executed.</param>
public class RelayCommand(Action<object?> execute, Predicate<object?> canExecute) : ICommand
{
    private bool destroyed;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The Action that needs to be executed when command is triggered.</param>
    public RelayCommand(Action<object?> execute)
        : this(execute, DefaultCanExecute)
    {
    }

    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
            CanExecuteChangedInternal += value;
        }

        remove
        {
            CommandManager.RequerySuggested -= value;
            CanExecuteChangedInternal -= value;
        }
    }

    private event EventHandler? CanExecuteChangedInternal;

    /// <summary>
    /// Gets a default empty <see cref="ICommand"/>.
    /// </summary>
    public static ICommand Empty
        => new RelayCommand(_ => { });

    /// <inheritdoc/>
    public bool CanExecute(object? parameter)
        => canExecute != null && !destroyed && canExecute(parameter);

    /// <summary>
    /// Destroy this <see cref="ICommand"/> so it wont trigger anymore.
    /// </summary>
    public void Destroy()
        => destroyed = true;

    /// <inheritdoc/>
    public void Execute(object? parameter)
    {
        if (destroyed)
        {
            return;
        }

        execute(parameter);
    }

    /// <summary>
    /// Trigger event that on execute has changed.
    /// </summary>
    public void OnCanExecuteChanged()
        => CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);

    private static bool DefaultCanExecute(object? parameter)
        => true;
}
