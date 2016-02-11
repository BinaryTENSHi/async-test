using System;

namespace AsyncTest.Communication.Server.Database
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}