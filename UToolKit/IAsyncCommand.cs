using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}