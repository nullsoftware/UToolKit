using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
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

        public RelaySingleTaskAsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelaySingleTaskAsyncCommand(Func<Task> execute) : this(execute, null)
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual bool CanExecute(object parameter)
        {
            if (IsExecuting)
                return false;

            return _canExecute == null ? true : _canExecute();
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

        public RelaySingleTaskAsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelaySingleTaskAsyncCommand(Func<T, Task> execute) : this(execute, null)
        {

        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual bool CanExecute(object parameter)
        {
            if (_currentTask != null)
                return false;

            return _canExecute == null ? true : _canExecute((T)parameter);
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
