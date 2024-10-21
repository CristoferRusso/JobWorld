namespace JobWorld.Models
{
    public class JobOffer
    {
        public required string Title { get; set; }
        public required string Company { get; set; }
        public required string Location { get; set; }
        public required string Description { get; set; }
        public required string Url { get; set; }
    }
}