using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.Abstraction.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
