using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncTest.Communication.Client.Communication.Queue;
using AsyncTest.Communication.Interface.Control;

namespace AsyncTest.Communication.Client.Communication.Control
{
    public class ControlPoller : AbstractAsyncPoller, IControlPoller
    {
        private readonly Uri _baseUri = new Uri("http://localhost:6688/");
        private readonly IQueuePoller _queuePoller;
        private readonly IRestClient _restClient;

        public ControlPoller(IRestClient restClient, IQueuePoller queuePoller)
        {
            _restClient = restClient;
            _queuePoller = queuePoller;
            SetInterval(TimeSpan.FromMilliseconds(100));
        }

        public override async Task PollAsync()
        {
            ControlRest controlRest =
                await _restClient.GetAsync<ControlRest>(new Uri(_baseUri, "/control/")).ConfigureAwait(false);

            if (!controlRest.ShouldPollQueue && _queuePoller.IsRunning)
            {
                Debug.WriteLine("Stopping QueuePoller...");
                _queuePoller.StopPolling();
            }
            else if (controlRest.ShouldPollQueue && !_queuePoller.IsRunning)
            {
                Debug.WriteLine("Starting QueuePoller...");
                _queuePoller.StartPolling();
            }
        }

        protected override void OnException(Exception exception)
        {
            Debug.WriteLine($"Exception Occurred: ${exception.GetType()}\n{exception}");
        }
    }
}