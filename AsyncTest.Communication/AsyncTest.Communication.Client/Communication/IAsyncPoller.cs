using System.Threading.Tasks;

namespace AsyncTest.Communication.Client.Communication
{
    public interface IAsyncPoller
    {
        bool IsRunning { get; }
        void StartPolling();

        void StopPolling();

        Task PollAsync();
    }
}