using AsyncTest.Communication.Client.Communication;
using Caliburn.Micro;

namespace AsyncTest.Communication.Client
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly IQueuePoller _queuePoller;

        public MainViewModel(IQueuePoller queuePoller)
        {
            _queuePoller = queuePoller;
        }

        protected override void OnInitialize()
        {
            DisplayName = "Communication Client";
            _queuePoller.StartPolling();
        }
    }
}