using System.Threading.Tasks;

namespace AsyncTest.CommandQueue.CommandQueue
{
    public interface ITaskQueue<in T>
        where T : IQueueTask
    {
        long ElapsedMilliseconds { get; }
        void Enqueue(T task);
        Task ExecuteAsync();
    }
}