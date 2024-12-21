using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.Abstraction.IRepositories
{
    public interface IUrlRepository : IRepository<Url>
    {
        Task<IEnumerable<Url>> GetAllWithDetailsAsync();

        Task<Url> GetByIdWithDetailsAsync(int id);
    }
}
