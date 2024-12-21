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

            await base.AddAsync(model);
        }

        protected override void Validate(UrlDto model)
        {
            throw new NotImplementedException();
        }
    }
}
