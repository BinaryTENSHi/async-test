using System;
using AsyncTest.Communication.Server.Database;

namespace AsyncTest.Communication.Server.Event
{
    public class EntityChangedEvent<T>
        where T : IEntity
    {
        public EntityChangedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}