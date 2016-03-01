using AsyncTest.Communication.Client.Communication.Control;
using AsyncTest.Communication.Client.Communication.Queue;
using Caliburn.Micro;

namespace AsyncTest.Communication.Client
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly IControlPoller _controlPoller;
        private readonly IQueuePoller _queuePoller;

        public MainViewModel(IQueuePoller queuePoller, IControlPoller controlPoller)
        {
            _queuePoller = queuePoller;
            _controlPoller = controlPoller;
        }

        protected override void OnInitialize()
        {
            DisplayName = "Communication Client";
            _controlPoller.StartPolling();
            _queuePoller.StartPolling();
        }
    }
}