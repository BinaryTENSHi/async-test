using AsyncTest.Shared.UI;

namespace AsyncTest.Communication.Server
{
    public interface IMainViewModel
    {
        IAsyncCommand CreateMessageQueueItemAsyncCommand { get; }
        string InputText { get; set; }
    }
}