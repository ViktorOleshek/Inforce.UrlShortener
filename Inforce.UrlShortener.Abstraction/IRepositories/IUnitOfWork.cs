namespace Inforce.UrlShortener.Abstraction.IRepositories
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }

        IUrlRepository UrlRepository { get; }

        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}
