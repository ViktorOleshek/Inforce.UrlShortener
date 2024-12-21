using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.DAL.Repositories;

namespace Inforce.UrlShortener.DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UrlShortenerContext dbContext;

        public UnitOfWork(UrlShortenerContext dbContext)
        {
            this.dbContext = dbContext;
            this.RoleRepository = new RoleRepository(dbContext);
            this.UrlRepository = new UrlRepository(dbContext);
            this.UserRepository = new UserRepository(dbContext);
        }

        public UnitOfWork(
            UrlShortenerContext dbContext,
            IRoleRepository customerRepository,
            IUrlRepository personRepository,
            IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.RoleRepository = customerRepository;
            this.UrlRepository = personRepository;
            this.UserRepository = userRepository;
        }

        public IRoleRepository RoleRepository { get; }
        public IUrlRepository UrlRepository { get; }
        public IUserRepository UserRepository { get; }

        public Task SaveAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }
    }
}
