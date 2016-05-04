using System;

namespace AsyncTest.Communication.Server.Database.Authentication
{
    public class ClientDto : IDto
    {
        public Guid Id { get; set; }
        public string SharedSecret { get; set; }
    }
}