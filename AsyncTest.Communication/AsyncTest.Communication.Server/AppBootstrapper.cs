using System;
using System.Windows;
using AsyncTest.Communication.Server.Server;
using AsyncTest.Shared;

namespace AsyncTest.Communication.Server
{
    public class AppBootstrapper : AsyncTestBootstrapper
    {
        private IRestServer _restServer;

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            MainKernel.Load<MainModule>();

            _restServer = MainKernel.Get<IRestServer>();
            _restServer.Start();

            DisplayRootViewFor<IMainViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            _restServer.Stop();
        }
    }
}