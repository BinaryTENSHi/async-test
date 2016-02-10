using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncTest.Shared.UI
{
    public interface IAsyncCommand : ICommand
    {
        bool IsExecuting { get; }

        Task ExecuteAsync(object parameter);
    }
}