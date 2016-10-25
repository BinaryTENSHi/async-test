using System;
using System.Threading.Tasks;

namespace AsyncTest.Pump
{
    public interface IQueueItem
    {
        Task HandleAsync();
    }

    public class FuncQueueItem : IQueueItem
    {
        private readonly Func<Task> _func;

        public FuncQueueItem(Func<Task> func)
        {
            _func = func;
        }

        public Task HandleAsync()
        {
            return _func();
        }
    }

    public class DelayQueueitem : IQueueItem
    {
        private readonly TimeSpan _delay;

        public DelayQueueitem(TimeSpan delay)
        {
            _delay = delay;
        }

        public Task HandleAsync()
        {
            return Task.Delay(_delay);
        }
    }
}