using System.ComponentModel;
using System.Threading.Tasks;

namespace AsyncTest.Shared.UI
{
    public delegate Task AsyncExecuteHandler(object parameter);

    public class AsyncCommand : DelegateCommand, IAsyncCommand, INotifyPropertyChanged
    {
        private bool _isExecuting;
        public new AsyncExecuteHandler ExecuteHandler { get; set; }

        public bool IsExecuting
        {
            get { return _isExecuting; }
            private set
            {
                if (_isExecuting == value)
                    return;

                _isExecuting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExecuting)));
            }
        }

        public override bool CanExecute(object parameter)
        {
            if (IsExecuting)
                return false;

            if (ExecuteHandler == null)
                return false;

            if (CanExecuteHandler == null)
                return true;

            return CanExecuteHandler(parameter);
        }

        public override async void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = true;
            await ExecuteAsync(parameter).ConfigureAwait(false);
            IsExecuting = false;
        }

        public Task ExecuteAsync(object parameter)
        {
            return ExecuteHandler(parameter);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}