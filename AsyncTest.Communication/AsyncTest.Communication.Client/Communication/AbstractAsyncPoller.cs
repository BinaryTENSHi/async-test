using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Client.Communication
{
    public abstract class AbstractAsyncPoller : IAsyncPoller, IDisposable
    {
        private Task _task;
        private TimeSpan _timeSpan;
        private CancellationTokenSource _token;

        protected AbstractAsyncPoller()
        {
            _token = new CancellationTokenSource();
        }

        public void StartPolling()
        {
            if (_timeSpan == null)
                throw new InvalidOperationException("Interval not set");

            _task = Task.Factory.StartNew(PollingTaskAsync, TaskCreationOptions.LongRunning);
        }

        public void StopPolling()
        {
            _token.Cancel();
        }

        public abstract Task PollAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void SetInterval(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        private async Task PollingTaskAsync()
        {
            while (true)
            {
                if (_token.IsCancellationRequested)
                    break;

                await PollAsync().ConfigureAwait(false);
                await Task.Delay(_timeSpan).ConfigureAwait(false);
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_token != null)
                {
                    _token.Dispose();
                    _token = null;
                }
            }
        }
    }
}