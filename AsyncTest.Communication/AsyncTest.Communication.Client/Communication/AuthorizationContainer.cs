namespace AsyncTest.Communication.Client.Communication
{
    public class AuthorizationContainer : IAuthorizationContainer
    {
        public string AppKey { get; set; } = string.Empty;
        public string SharedSecret { get; set; } = string.Empty;
    }
}