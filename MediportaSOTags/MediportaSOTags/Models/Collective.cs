namespace MediportaSOTags.Models
{
    public class Collective
    {
        public IList<string> tags { get; set; }
        public IList<ExternalLink> external_links { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }
}
