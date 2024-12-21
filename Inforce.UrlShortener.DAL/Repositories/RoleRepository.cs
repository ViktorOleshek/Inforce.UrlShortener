using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.DAL.Data;
using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.DAL.Repositories
{
    public class RoleRepository : AbstractRepository<Role>, IRoleRepository
    {
        public RoleRepository(UrlShortenerContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
        }
    }
}
