using Owin;

namespace AsyncTest.Communication.Server.Server
{
    public interface IRestServer
    {
        void Startup(IAppBuilder appBuilder);
        void Start();
        void Stop();
    }
}