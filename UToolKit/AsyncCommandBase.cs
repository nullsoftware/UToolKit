using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
    /// <summary>
    /// Basic abstract async windows input command.
    /// </summary>
    public abstract class AsyncCommandBase : IAsyncCommand, IRefreshableCommand
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

        #region Methods

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract Task ExecuteAsync(object parameter);

        /// <inheritdoc/>
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /// <inheritdoc/>
        public virtual void Refresh()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
