namespace AsyncTest.Communication.Server.Service
{
    public interface IControlService
    {
        bool ShouldPoll { get; set; }
    }
}