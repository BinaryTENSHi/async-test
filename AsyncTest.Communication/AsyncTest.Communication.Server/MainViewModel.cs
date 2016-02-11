using System;
using System.Threading.Tasks;
using AsyncTest.Communication.Interface.Queue;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;
using AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem;
using AsyncTest.Shared.UI;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly IMessageQueueItemRepository _messageQueueItemRepository;
        private string _inputText;

        public MainViewModel(IMessageQueueItemRepository messageQueueItemRepository)
        {
            _messageQueueItemRepository = messageQueueItemRepository;

            CreateMessageQueueItemAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => !string.IsNullOrWhiteSpace(InputText),
                ExecuteHandler = CreateMessageQueueItemAsync
            };
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

        private async Task CreateMessageQueueItemAsync(object parameter)
        {
            MessageQueueItemDto item = new MessageQueueItemDto
            {
                Id = Guid.NewGuid(),
                ItemType = QueueItemType.Message,
                Message = InputText
            };

            _messageQueueItemRepository.Insert(item);
            await _messageQueueItemRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnInitialize()
        {
            DisplayName = "Communication Server";
        }
    }
}