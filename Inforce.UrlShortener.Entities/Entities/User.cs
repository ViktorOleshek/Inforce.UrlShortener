namespace Inforce.UrlShortener.Entities
{
    public class User : BaseEntity
    {
        public User()
            : base()
        {
        }

        public User(int id)
            : base(id)
        {
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Url> Urls { get; set; } = new HashSet<Url>();
    }
}
