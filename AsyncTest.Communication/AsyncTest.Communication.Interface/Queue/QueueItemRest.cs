using System;

namespace AsyncTest.Communication.Interface.Queue
{
    public class QueueItemRest
    {
        public QueueItemType ItemType { get; set; }
        public Guid Id { get; set; }
    }
}