namespace Inforce.UrlShortener.DAL.Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            this.Id = 0;
        }

        protected BaseEntity(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
