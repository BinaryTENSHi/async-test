using System.Windows;
using AsyncTest.Shared;

namespace AsyncTest.Communication.Client
{
    public class AppBootstrapper : AsyncTestBootstrapper
    {
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            MainKernel.Load<MainModule>();
            DisplayRootViewFor<IMainViewModel>();
        }
    }
}