using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsyncTest.Communication.Interface.Queue;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;
using AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem;
using AsyncTest.Communication.Server.Event;
using AsyncTest.Communication.Server.Service;
using AsyncTest.Shared.UI;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server
{
    public class MainViewModel : Screen, IMainViewModel, IHandleWithTask<EntityChangedEvent<QueueItemEntity>>
    {
        private readonly IControlService _controlService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IQueueItemRepository _queueItemRepository;
        private string _inputText;

        public MainViewModel(
            IQueueItemRepository queueItemRepository,
            IEventAggregator eventAggregator,
            IControlService controlService)
        {
            _queueItemRepository = queueItemRepository;
            _eventAggregator = eventAggregator;
            _controlService = controlService;

            CreateMessageQueueItemAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => !string.IsNullOrWhiteSpace(InputText),
                ExecuteHandler = CreateMessageQueueItemAsync
            };

            QueueItems = new ObservableCollection<QueueItemDto>();
        }

        public Task Handle(EntityChangedEvent<QueueItemEntity> message)
        {
            return Execute.OnUIThreadAsync(async () => await UpdateQueueItemsAsync().ConfigureAwait(false));
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                NotifyOfPropertyChange(nameof(InputText));
            }
        }

        public IAsyncCommand CreateMessageQueueItemAsyncCommand { get; }

        public ObservableCollection<QueueItemDto> QueueItems { get; set; }

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

        protected override void OnInitialize()
        {
            DisplayName = "Communication Server";
        }

        protected override async void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            await UpdateQueueItemsAsync().ConfigureAwait(false);
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
    }
}