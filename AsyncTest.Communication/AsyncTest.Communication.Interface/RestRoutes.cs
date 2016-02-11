using System;

namespace AsyncTest.Communication.Interface
{
    public static class RestRoutes
    {
        public const string ServiceDescriptionUrl = "";
        public const string VersionUrl = "version/";

        public const string QueueUrl = "queue/";
        public const string QueueItemUrl = QueueUrl + "{id:guid}/";

        public static LinkRest MakeQueueItemLink(Guid id) =>
            new LinkRest(RestRelations.QueueItemRelation, "/" + QueueItemUrl.Replace("{id:guid}", id.ToString()));
    }
}