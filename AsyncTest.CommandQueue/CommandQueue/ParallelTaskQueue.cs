using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTest.CommandQueue.CommandQueue
{
    public class ParallelTaskQueue<T> : AbstractTaskQueue<T>
        where T : IQueueTask
    {
        public ParallelTaskQueue(IEnumerable<T> tasks)
            : base(tasks)
        {
        }

        public override async Task ExecuteAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();

            List<Task> tasks = Tasks.Select(x => x.ExecuteAsync()).ToList();
            await Task.WhenAll(tasks).ConfigureAwait(false);

            watch.Stop();
            ElapsedMilliseconds = watch.ElapsedMilliseconds;
        }
    }
}