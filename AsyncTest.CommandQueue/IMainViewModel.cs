using System.Collections.ObjectModel;
using System.Windows.Input;
using AsyncTest.Shared.UI;

namespace AsyncTest.CommandQueue
{
    public interface IMainViewModel
    {
        string InputText { get; set; }
        ObservableCollection<PrintTask> Tasks { get; }
        ICommand EnqueueCommand { get; }
        IAsyncCommand RunParallelAsyncCommand { get; }
        IAsyncCommand RunSerialAsyncCommand { get; }
        string LogText { get; set; }
    }
}