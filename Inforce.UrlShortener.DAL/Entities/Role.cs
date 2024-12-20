namespace Inforce.UrlShortener.DAL.Entities
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
