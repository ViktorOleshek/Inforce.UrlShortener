namespace Inforce.UrlShortener.Entities
{
    public class Role : BaseEntity
    {
        public Role()
            : base()
        {
        }

        public Role(int id)
            : base(id)
        {
        }

        public string RoleName { get; set; }
    }
}
