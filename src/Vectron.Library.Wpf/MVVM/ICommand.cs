using System.Windows.Input;

namespace Vectron.Library.Wpf.MVVM;

/// <summary>
/// A generic <see cref="ICommand"/> implementation.
/// </summary>
/// <typeparam name="T">The type of passed parameter.</typeparam>
public interface ICommand<T> : ICommand
{
    /// <inheritdoc cref="ICommand.CanExecute(object)"/>
    bool CanExecute(T parameter);

    /// <inheritdoc cref="ICommand.Execute(object)"/>
    void Execute(T parameter);
}
