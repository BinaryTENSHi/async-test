using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsyncTest.Communication.Interface.Authentication;
using AsyncTest.Communication.Interface.Queue;
using AsyncTest.Communication.Server.Database.Authentication;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;
using AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem;
using AsyncTest.Communication.Server.Event;
using AsyncTest.Communication.Server.Service;
using AsyncTest.Shared.UI;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server
{
    public class MainViewModel : Screen, IMainViewModel, IHandleWithTask<EntityChangedEvent<QueueItemEntity>>, IHandleWithTask<EntityChangedEvent<ClientEntity>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IControlService _controlService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IQueueItemRepository _queueItemRepository;
        private string _inputText;

        public MainViewModel(
            IQueueItemRepository queueItemRepository,
            IEventAggregator eventAggregator,
            IControlService controlService,
            IClientRepository clientRepository)
        {
            _queueItemRepository = queueItemRepository;
            _eventAggregator = eventAggregator;
            _controlService = controlService;
            _clientRepository = clientRepository;

            CreateMessageQueueItemAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => !string.IsNullOrWhiteSpace(InputText),
                ExecuteHandler = CreateMessageQueueItemAsync
            };

            CreateClientAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => true,
                ExecuteHandler = CreateClientAsync
            };

            QueueItems = new ObservableCollection<QueueItemDto>();
            Clients = new ObservableCollection<ClientDto>();
        }

        public IAsyncCommand CreateClientAsyncCommand { get; }
        public IAsyncCommand CreateMessageQueueItemAsyncCommand { get; }

        public Task Handle(EntityChangedEvent<QueueItemEntity> message)
            => Execute.OnUIThreadAsync(async () => await UpdateQueueItemsAsync().ConfigureAwait(false));

        public Task Handle(EntityChangedEvent<ClientEntity> message) 
            => Execute.OnUIThreadAsync(async () => await UpdateClientsAsync().ConfigureAwait(false));

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                NotifyOfPropertyChange(nameof(InputText));
            }
        }

        public ObservableCollection<QueueItemDto> QueueItems { get; set; }
        public ObservableCollection<ClientDto> Clients { get; set; }

        public bool ShouldPoll
        {
            get { return _controlService.ShouldPoll; }
            set { _controlService.ShouldPoll = value; }
        }

        private async Task CreateMessageQueueItemAsync(object parameter)
        {
            MessageQueueItemDto item = new MessageQueueItemDto
            {
                Id = Guid.NewGuid(),
                ItemType = QueueItemType.Message,
                Message = InputText
            };

            _queueItemRepository.Insert(item);
            await _queueItemRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task CreateClientAsync(object parameter)
        {
            ClientDto item = new ClientDto
            {
                Id = Guid.NewGuid(),
                SharedSecret = Convert.ToBase64String(AuthenticationHelper.SecureBytes(64))
            };

            _clientRepository.Insert(item);
            await _clientRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnInitialize()
        {
            DisplayName = "Communication Server";
        }

        protected override async void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            await UpdateQueueItemsAsync().ConfigureAwait(false);
            await UpdateClientsAsync().ConfigureAwait(false);
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
        }

        private async Task UpdateQueueItemsAsync()
        {
            QueueItems.Clear();
            foreach (QueueItemDto queueItemDto in await _queueItemRepository.AllAsync().ConfigureAwait(true))
                QueueItems.Add(queueItemDto);
        }

        private async Task UpdateClientsAsync()
        {
            Clients.Clear();
            foreach (ClientDto clientDto in await _clientRepository.AllAsync().ConfigureAwait(true))
                Clients.Add(clientDto);
        }
    }
}