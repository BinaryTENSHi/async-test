using System.Collections.ObjectModel;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;
using AsyncTest.Shared.UI;

namespace AsyncTest.Communication.Server
{
    public interface IMainViewModel
    {
        IAsyncCommand CreateMessageQueueItemAsyncCommand { get; }
        string InputText { get; set; }
        ObservableCollection<QueueItemDto> QueueItems { get; set; }
    }
}