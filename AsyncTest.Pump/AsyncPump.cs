using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest.Pump
{
    public class AsyncPump
    {
        private readonly ConcurrentDictionary<Task, Task> _runningTasks = new ConcurrentDictionary<Task, Task>();
        private readonly SemaphoreSlim _semaphore;
        private readonly ConcurrentQueue<IQueueItem> _taskQueue = new ConcurrentQueue<IQueueItem>();
        private readonly CancellationTokenSource _tokenSource;
        private long _messageCount;
        private Task _pumpingTask;

        public AsyncPump(int concurrency)
        {
            _semaphore = new SemaphoreSlim(concurrency);
            _tokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            _pumpingTask = Task.Run(async () =>
            {
                while (!_tokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        await _semaphore.WaitAsync(_tokenSource.Token).ConfigureAwait(false);

                        Task task = HandleMessageAsync();
                        _runningTasks.TryAdd(task, task);
                        task.ContinueWith(
                            t =>
                            {
                                Interlocked.Increment(ref _messageCount);
                                _semaphore.Release();
                                _runningTasks.TryRemove(t, out t);
                            }, TaskContinuationOptions.ExecuteSynchronously).Ignore();
                    }
                    catch (OperationCanceledException)
                    {
                        // ignore
                    }
                }
            });
        }

        public void Enqueue(IQueueItem item)
        {
            _taskQueue.Enqueue(item);
        }

        private Task HandleMessageAsync()
        {
            IQueueItem item;
            if (_taskQueue.TryDequeue(out item))
                return item.HandleAsync();

            return Task.CompletedTask;
        }

        public async Task StopAsync(TimeSpan timeout)
        {
            Console.WriteLine("Setting cancellation token...");
            _tokenSource.Cancel();

            Console.WriteLine("Awaiting remaining tasks...");
            Task timeoutTask = Task.Delay(timeout);
            Task remainingTask = Task.WhenAll(_runningTasks.Values);
            await Task.WhenAny(remainingTask, timeoutTask).ConfigureAwait(false);

            Console.WriteLine("Awaiting pumping task...");
            await _pumpingTask.ConfigureAwait(false);

            Console.WriteLine($"Processed {_messageCount} messages");
        }
    }
}