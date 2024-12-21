namespace Inforce.UrlShortener.Entities
{
    public class Url : BaseEntity
    {
        public Url()
            : base()
        {
        }

        public Url(int id)
            : base(id)
        {
        }

        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
