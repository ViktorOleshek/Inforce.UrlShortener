using Inforce.UrlShortener.Abstraction.DTOs;

namespace Inforce.UrlShortener.Abstraction.IServices
{
    public interface IUserService : ICrud<UserDto>
    {
        Task<UserDto> AuthenticateAsync(string username, string password);
        Task<UserDto> GetByUsernameAsync(string username);
    }
}
