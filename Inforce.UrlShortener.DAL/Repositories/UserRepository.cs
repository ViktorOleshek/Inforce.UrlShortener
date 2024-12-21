using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.DAL.Data;
using Inforce.UrlShortener.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inforce.UrlShortener.DAL.Repositories
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(UrlShortenerContext context)
            : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await this.Context.Set<User>()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == username);
        }
    }
}
