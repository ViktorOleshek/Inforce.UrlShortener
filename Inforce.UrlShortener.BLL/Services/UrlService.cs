using AutoMapper;
using Inforce.UrlShortener.Abstraction.DTOs;
using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.Abstraction.IServices;
using Inforce.UrlShortener.BLL.Validation;
using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.BLL.Services
{
    public class UrlService : BaseService<Url, UrlDto>, IUrlService
    {
        private readonly IUrlRepository urlRepository;
        public UrlService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.urlRepository = unitOfWork.UrlRepository;
        }

        protected override IRepository<Url> Repository { get => this.urlRepository; }

        public Task<UrlDto> GenerateShortUrlAsync(string originalUrl)
        {
            throw new NotImplementedException();
        }

        public override async Task AddAsync(UrlDto model)
        {
            var entity = await this.UnitOfWork.UrlRepository.GetByIdAsync(model.Id);
            if (entity != null)
            {
                throw new MarketException("Url is already taken.");
            }
            //отут треба викликати GenerateShortUrlAsync і засувати в модель дані
            await base.AddAsync(model);
        }

        protected override void Validate(UrlDto model)
        {
            // Перевірка на наявність оригінального URL
            if (string.IsNullOrWhiteSpace(model.OriginalUrl))
            {
                throw new MarketException("Original URL is required.");
            }

            // Перевірка на правильний формат URL
            //if (!Uri.IsWellFormedUriString(model.OriginalUrl, UriKind.Absolute))
            //{
            //    throw new MarketException("Original URL is not valid.");
            //}

            // Перевірка на наявність скороченого URL
            if (string.IsNullOrWhiteSpace(model.ShortUrl))
            {
                throw new MarketException("Short URL is required.");
            }

            // Перевірка на дату створення (якщо потрібно)
            if (model.CreatedDate > DateTime.Now)
            {
                throw new MarketException("Created date cannot be in the future.");
            }

            // Додаткові перевірки можуть бути додані, наприклад:
            // Перевірка чи є CreatedBy (якщо необхідно)
            if (string.IsNullOrWhiteSpace(model.CreatedBy))
            {
                throw new MarketException("Creator's name is required.");
            }
        }

    }
}
