using System;
using System.Threading.Tasks;

namespace NullSoftware
{
    public class RelayAsyncCommand : AsyncCommandBase
    {
        #region Fields

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        #endregion

        #region Constructors

        public RelayAsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

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

    public class RelayAsyncCommand<T> : AsyncCommandBase
    {
        #region Fields

        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        #endregion

        #region Constructors

        public RelayAsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException(nameof(execute));
            if (canExecute == null)
                throw new NullReferenceException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

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
