using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
    /// <summary>
    /// Async delegate windows input command, designed for single task execution.
    /// </summary>
    /// <remarks>
    /// After <see cref="ICommand.Execute(object)"/> method invoke,
    /// the <see cref="ICommand.CanExecute(object)"/> method will return only <c>false</c>
    /// while current async task is executing
    /// (it can be monitored using <see cref="RelaySingleTaskAsyncCommand.IsExecuting"/>).
    /// </remarks>
    public class RelaySingleTaskAsyncCommand : ObservableObject, IAsyncCommand, IRefreshableCommand
    {
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

        #region Fields

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        private Task _currentTask;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value that indicates whether the current command is executing.
        /// </summary>
        public bool IsExecuting => _currentTask != null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RelaySingleTaskAsyncCommand"/> class,
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
        public RelaySingleTaskAsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelaySingleTaskAsyncCommand"/> class,
        /// using specified action to invoke on execution.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> is null.
        /// </exception>
        public RelaySingleTaskAsyncCommand(Func<Task> execute) : this(execute, new Func<bool>(() => true))
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual bool CanExecute(object parameter)
        {
            if (IsExecuting)
                return false;

            return _canExecute();
        }

        /// <inheritdoc/>
        public virtual Task ExecuteAsync(object parameter)
        {
            return _execute();
        }

        /// <inheritdoc/>
        public async void Execute(object parameter)
        {
            _currentTask = ExecuteAsync(parameter);
            RaisePropertyChanged(nameof(IsExecuting));
            Refresh();

            try
            {
                await _currentTask;
            }
            finally
            {
                _currentTask = null;
                RaisePropertyChanged(nameof(IsExecuting));
                Refresh();
            }
        }

        /// <inheritdoc/>
        public virtual void Refresh()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

    /// <summary>
    /// Async delegate windows input command with parameter support, 
    /// designed for single task execution.
    /// </summary>
    /// <typeparam name="T">Type of command parameter.</typeparam>
    /// <remarks>
    /// After <see cref="ICommand.Execute(object)"/> method invoke,
    /// the <see cref="ICommand.CanExecute(object)"/> method will return only <c>false</c>
    /// while current async task is executing
    /// (it can be monitored using <see cref="RelaySingleTaskAsyncCommand.IsExecuting"/>).
    /// </remarks>
    public class RelaySingleTaskAsyncCommand<T> : ObservableObject, IAsyncCommand, IRefreshableCommand
    {
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

        #region Fields

        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        private Task _currentTask;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value that indicates whether the current command is executing.
        /// </summary>
        public bool IsExecuting => _currentTask != null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RelaySingleTaskAsyncCommand{T}"/> class,
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
        public RelaySingleTaskAsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelaySingleTaskAsyncCommand{T}"/> class,
        /// using specified action to invoke on execution with specified parameter type.
        /// </summary>
        /// <param name="execute">
        /// The action to invoke when <see cref="ICommand.Execute"/> is called.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws when <paramref name="execute"/> is null.
        /// </exception>
        public RelaySingleTaskAsyncCommand(Func<T, Task> execute) : this(execute, new Func<T, bool>((_) => true))
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual bool CanExecute(object parameter)
        {
            if (IsExecuting)
                return false;

            return _canExecute((T)parameter);
        }

        /// <inheritdoc/>
        public virtual Task ExecuteAsync(object parameter)
        {
            return _execute((T)parameter);
        }

        /// <inheritdoc/>
        public async void Execute(object parameter)
        {
            _currentTask = ExecuteAsync(parameter);
            RaisePropertyChanged(nameof(IsExecuting));
            Refresh();

            try
            {
                await _currentTask;
            }
            finally
            {
                _currentTask = null;
                RaisePropertyChanged(nameof(IsExecuting));
                Refresh();
            }
        }

        /// <inheritdoc/>
        public virtual void Refresh()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
