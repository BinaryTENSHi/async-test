namespace AsyncTest.Communication.Interface
{
    public class LinkRest
    {
        public LinkRest(string relation, string href)
        {
            Relation = relation;
            Href = href;
        }

        public string Relation { get; }
        public string Href { get; }
    }
}