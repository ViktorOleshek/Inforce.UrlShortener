using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.DAL.Data;
using Inforce.UrlShortener.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inforce.UrlShortener.DAL.Repositories
{
    public class UrlRepository : AbstractRepository<Url>, IUrlRepository
    {
        public UrlRepository(UrlShortenerContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
        }

        public async Task<IEnumerable<Url>> GetAllWithDetailsAsync()
        {
            return await this.Context.Set<Url>()
                .Include(e => e.User)
                    .ThenInclude(u => u.Role)
                .ToListAsync();
        }

        public async Task<Url> GetByIdWithDetailsAsync(int id)
        {
            return await this.Context.Set<Url>()
                .Include(e => e.User)
                    .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
