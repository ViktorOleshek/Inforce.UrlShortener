using Inforce.UrlShortener.Abstraction.DTOs;

namespace Inforce.UrlShortener.Abstraction.IServices
{
    public interface IUrlService : ICrud<UrlDto>
    {
        Task<UrlDto> GenerateShortUrlAsync(string originalUrl);
    }
}
