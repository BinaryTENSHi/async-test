using System;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemDto : IDto
    {
        public QueueItemType ItemType { get; set; }
        public Guid Id { get; set; }
    }
}