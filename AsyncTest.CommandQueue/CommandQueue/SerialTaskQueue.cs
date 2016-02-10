using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncTest.CommandQueue.CommandQueue
{
    public class SerialTaskQueue<T> : AbstractTaskQueue<T>
        where T : IQueueTask
    {
        public SerialTaskQueue(IEnumerable<T> tasks) : base(tasks)
        {
        }

        public override async Task ExecuteAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();

            foreach (T task in Tasks)
            {
                await task.ExecuteAsync().ConfigureAwait(false);
            }

            watch.Stop();
            ElapsedMilliseconds = watch.ElapsedMilliseconds;
        }
    }
}