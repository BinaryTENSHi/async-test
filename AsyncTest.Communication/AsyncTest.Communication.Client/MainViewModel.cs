using AsyncTest.Communication.Client.Communication;
using AsyncTest.Communication.Client.Communication.Control;
using Caliburn.Micro;

namespace AsyncTest.Communication.Client
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly IAuthorizationContainer _container;
        private readonly IControlPoller _controlPoller;
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel(
            IControlPoller controlPoller,
            IAuthorizationContainer container,
            IEventAggregator eventAggregator)
        {
            _controlPoller = controlPoller;
            _container = container;
            _eventAggregator = eventAggregator;
        }

        public string AppKey
        {
            get { return _container.AppKey; }
            set
            {
                _container.AppKey = value;
                NotifyOfPropertyChange(nameof(AppKey));
            }
        }

        public string SharedSecret
        {
            get { return _container.SharedSecret; }
            set
            {
                _container.SharedSecret = value;
                NotifyOfPropertyChange(nameof(SharedSecret));
            }
        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        protected override void OnInitialize()
        {
            DisplayName = "Communication Client";
            _controlPoller.StartPolling();
        }
    }
}