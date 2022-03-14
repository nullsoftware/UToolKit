using System.Windows.Input;

namespace NullSoftware
{
    /// <summary>
    /// Refreshable async command, wehere can be manually raised <see cref="ICommand.CanExecuteChanged"/> event.
    /// </summary>
    public interface IRefreshableAsyncCommand : IAsyncCommand
    {
        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/> event.
        /// </summary>
        void Refresh();
    }
}