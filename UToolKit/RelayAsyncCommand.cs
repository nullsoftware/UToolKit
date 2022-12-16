using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
    /// <summary>
    /// Async delegate windows input command.
    /// </summary>
    public class RelayAsyncCommand : AsyncCommandBase
    {
        #region Fields

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RelayAsyncCommand"/> class,
        /// using specified action to invoke on execution
        /// and a function to query for determining if the command can execute.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <param name="canExecute">
        /// The function to invoke when <see cref="ICommand.CanExecute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> or <paramref name="canExecute"/> is null.
        /// </exception>
        public RelayAsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelayAsyncCommand"/> class,
        /// using specified action to invoke on execution.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> is null.
        /// </exception>
        public RelayAsyncCommand(Func<Task> execute) : this(execute, new Func<bool>(() => true))
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        /// <inheritdoc/>
        public override Task ExecuteAsync(object parameter)
        {
            return _execute();
        }

        #endregion
    }

    /// <summary>
    /// Async delegate windows input command with parameter support.
    /// </summary>
    /// <typeparam name="T">Type of command parameter.</typeparam>
    public class RelayAsyncCommand<T> : AsyncCommandBase
    {
        #region Fields

        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RelayAsyncCommand{T}"/> class,
        /// using specified action to invoke on execution with specified parameter type
        /// and a function to query for determining if the command can execute.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <param name="canExecute">
        /// The function to invoke when <see cref="ICommand.CanExecute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> or <paramref name="canExecute"/> is null.
        /// </exception>
        public RelayAsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelayAsyncCommand{T}"/> class,
        /// using specified action to invoke on execution with specified parameter type.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> is null.
        /// </exception>
        public RelayAsyncCommand(Func<T, Task> execute) : this(execute, new Func<T, bool>((_) => true))
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return _canExecute((T)parameter);
        }

        /// <inheritdoc/>
        public override Task ExecuteAsync(object parameter)
        {
            return _execute((T)parameter);
        }

        #endregion
    }
}
