using System.Collections.Generic;

namespace AsyncTest.Communication.Interface.Queue
{
    public class QueueRest
    {
        public QueueRest()
        {
            Items = new List<LinkRest>();
        }

        public IList<LinkRest> Items { get; }
    }
}