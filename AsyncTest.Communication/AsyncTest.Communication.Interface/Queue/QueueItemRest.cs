using System;

namespace AsyncTest.Communication.Interface.Queue
{
    public class QueueItemRest
    {
        public QueueItemRest(Guid thingy)
        {
            Thingy = thingy;
        }

        public Guid Thingy { get; }
    }
}