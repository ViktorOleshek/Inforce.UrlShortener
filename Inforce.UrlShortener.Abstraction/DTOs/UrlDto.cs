namespace Inforce.UrlShortener.Abstraction.DTOs
{
    public class UrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatorId { get; set; }
        public string CreatedBy { get; set; }
    }
}
