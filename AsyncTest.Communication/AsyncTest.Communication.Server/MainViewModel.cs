using Caliburn.Micro;

namespace AsyncTest.Communication.Server
{
    public class MainViewModel : Screen, IMainViewModel
    {
        protected override void OnInitialize()
        {
            DisplayName = "Communication Server";
        }
    }
}