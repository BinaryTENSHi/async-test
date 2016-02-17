using System.Threading.Tasks;

namespace AsyncTest.Communication.Client.Communication
{
    public interface IAsyncPoller
    {
        void StartPolling();

        void StopPolling();

        Task PollAsync();
    }
}