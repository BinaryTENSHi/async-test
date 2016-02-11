using System;
using AsyncTest.Communication.Interface.Queue;

namespace AsyncTest.Communication.Interface
{
    public static class RestRoutes
    {
        public const string ServiceDescriptionUrl = "";
        public const string VersionUrl = "version/";

        public const string QueueUrl = "queue/";
        public const string QueueItemUrl = QueueUrl + "{id:guid}/";

        public static LinkRest MakeQueueItemLink(QueueItemType itemType, Guid id)
        {
            string relation;
            switch (itemType)
            {
                case QueueItemType.Message:
                    relation = RestRelations.MessageQueueItemRelation;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
            }

            return new LinkRest(relation, "/" + QueueItemUrl.Replace("{id:guid}", id.ToString()));
        }
    }
}