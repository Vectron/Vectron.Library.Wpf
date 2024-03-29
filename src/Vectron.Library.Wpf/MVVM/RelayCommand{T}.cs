using System.Windows.Input;

namespace Vectron.Library.Wpf.MVVM;

/// <summary>
/// A <see cref="ICommand"/> to bind to commands.
/// </summary>
/// <typeparam name="T">The type of passed parameter.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
/// </remarks>
/// <param name="execute">The Action that needs to be executed when command is triggered.</param>
/// <param name="canExecute">The action to check if the command can be executed.</param>
public class RelayCommand<T>(Action<T?> execute, Predicate<T?> canExecute) : ICommand<T>
{
    private bool destroyed;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
    /// </summary>
    /// <param name="execute">The Action that needs to be executed when command is triggered.</param>
    public RelayCommand(Action<T?> execute)
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

    /// <inheritdoc/>
    public bool CanExecute(object? parameter)
        => parameter == null
        ? CanExecute((T?)parameter)
        : parameter is not T converted
            ? throw new ArgumentException("Parameter if of wrong type", nameof(parameter))
            : CanExecute(converted);

    /// <inheritdoc/>
    public bool CanExecute(T? parameter)
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

        if (parameter == null)
        {
            Execute((T?)parameter);
            return;
        }

        if (parameter is not T converted)
        {
            throw new ArgumentException("Parameter if of wrong type", nameof(parameter));
        }

        Execute(converted);
    }

    /// <inheritdoc/>
    public void Execute(T? parameter)
        => execute(parameter);

    /// <summary>
    /// Trigger event that on execute has changed.
    /// </summary>
    public void OnCanExecuteChanged()
        => CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);

    private static bool DefaultCanExecute(T? parameter)
        => true;
}
