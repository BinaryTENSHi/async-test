using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace AsyncTest.Shared.UI
{
    public class DelegateCommand : ICommand
    {
        private EventHandler _canExecuteChanged;

        public Func<object, bool> CanExecuteHandler { get; set; }
        public Action<object> ExecuteHandler { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _canExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;

                // ReSharper disable once DelegateSubtraction
                _canExecuteChanged -= value;
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            if (ExecuteHandler == null)
                return false;

            if (CanExecuteHandler == null)
                return true;

            return CanExecuteHandler(parameter);
        }

        public virtual void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            ExecuteHandler?.Invoke(parameter);
        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}