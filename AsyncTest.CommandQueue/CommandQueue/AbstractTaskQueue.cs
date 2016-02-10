using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTest.CommandQueue.CommandQueue
{
    public abstract class AbstractTaskQueue<T> : ITaskQueue<T>
        where T : IQueueTask
    {
        protected AbstractTaskQueue()
        {
            Tasks = new List<T>();
        }

        protected AbstractTaskQueue(IEnumerable<T> tasks)
        {
            Tasks = tasks.ToList();
        }

        protected IList<T> Tasks { get; }
        public long ElapsedMilliseconds { get; protected set; }

        public void Enqueue(T task)
        {
            Tasks.Add(task);
        }

        public abstract Task ExecuteAsync();
    }
}