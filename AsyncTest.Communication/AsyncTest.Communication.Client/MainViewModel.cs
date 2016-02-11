using Caliburn.Micro;

namespace AsyncTest.Communication.Client
{
    public class MainViewModel : Screen, IMainViewModel
    {
        protected override void OnInitialize()
        {
            DisplayName = "Communication Client";
        }
    }
}