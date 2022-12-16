using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
    /// <summary>
    /// Defines a async command.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>
        /// The task which executes.
        /// </returns>
        Task ExecuteAsync(object parameter);
    }
}