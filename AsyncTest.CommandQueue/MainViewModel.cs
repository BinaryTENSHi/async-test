using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncTest.CommandQueue.CommandQueue;
using AsyncTest.Shared.UI;
using Caliburn.Micro;

namespace AsyncTest.CommandQueue
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private string _inputText;
        private string _logText;

        public MainViewModel()
        {
            Tasks = new ObservableCollection<PrintTask>();
            for (int i = 0; i < 5000; i++)
            {
                Tasks.Add(new PrintTask($"{i}"));
            }

            EnqueueCommand = new DelegateCommand
            {
                CanExecuteHandler = obj => !string.IsNullOrEmpty(InputText),
                ExecuteHandler = Enqueue
            };

            RunParallelAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => Tasks.Any(),
                ExecuteHandler = RunParallelAsync
            };

            RunSerialAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => Tasks.Any(),
                ExecuteHandler = RunSerialAsync
            };
        }

        public string LogText
        {
            get { return _logText; }
            set
            {
                _logText = value;
                NotifyOfPropertyChange(nameof(LogText));
            }
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                NotifyOfPropertyChange(nameof(InputText));
                Execute.OnUIThreadAsync(CommandManager.InvalidateRequerySuggested);
            }
        }

        public ObservableCollection<PrintTask> Tasks { get; }

        public ICommand EnqueueCommand { get; }
        public IAsyncCommand RunParallelAsyncCommand { get; }

        public IAsyncCommand RunSerialAsyncCommand { get; }

        private async Task RunSerialAsync(object parameter)
        {
            SerialTaskQueue<PrintTask> taskQueue = new SerialTaskQueue<PrintTask>(Tasks);
            await taskQueue.ExecuteAsync().ConfigureAwait(true);

            LogText = $"Queue took {taskQueue.ElapsedMilliseconds}ms";

            CommandManager.InvalidateRequerySuggested();
        }

        private async Task RunParallelAsync(object parameter)
        {
            ParallelTaskQueue<PrintTask> taskQueue = new ParallelTaskQueue<PrintTask>(Tasks);
            await taskQueue.ExecuteAsync().ConfigureAwait(true);

            LogText = $"Queue took {taskQueue.ElapsedMilliseconds}ms";

            CommandManager.InvalidateRequerySuggested();
        }

        private void Enqueue(object obj)
        {
            Tasks.Add(new PrintTask(InputText));
            InputText = string.Empty;
        }

        protected override void OnInitialize()
        {
            DisplayName = "CommandQueue Test";
        }
    }

    public class PrintTask : IQueueTask
    {
        public PrintTask(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public Task ExecuteAsync()
        {
            return Task.Run(() => Debug.WriteLine(Content));
        }
    }

    public interface IQueueTask
    {
        Task ExecuteAsync();
    }
}