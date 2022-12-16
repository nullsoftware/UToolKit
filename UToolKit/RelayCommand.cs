using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
    /// <summary>
    /// Basic implementation of implementing a windows input command.
    /// </summary>
    public class RelayCommand : IRefreshableCommand
    {
        #region Fields

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        #endregion

        #region Events

        private event EventHandler _canExecuteChanged;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _canExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/> class,
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
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/> class,
        /// using specified action to invoke on execution.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> is null.
        /// </exception>
        public RelayCommand(Action execute) : this(execute, new Func<bool>(() => true))
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            _execute.Invoke();
        }

        /// <inheritdoc/>
        public void Refresh()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

    /// <summary>
    /// Basic implementation of implementing a windows input command with parameter support.
    /// </summary>
    /// <typeparam name="T">Type of command parameter.</typeparam>
    public class RelayCommand<T> : IRefreshableCommand
    {
        #region Fields

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        #endregion

        #region Events

        private event EventHandler _canExecuteChanged;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _canExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand{T}"/> class,
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
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand{T}"/> class,
        /// using specified action to invoke on execution with specified parameter type.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> is null.
        /// </exception>
        public RelayCommand(Action<T> execute) : this(execute, new Func<T, bool>((_) => true))
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return _canExecute((T)parameter);
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            _execute.Invoke((T)parameter);
        }

        /// <inheritdoc/>
        public void Refresh()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
