using AutoMapper;
using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.Abstraction.IServices;
using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.BLL.Services
{
    public abstract class BaseService<TEntity, TDto> : ICrud<TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        protected abstract IRepository<TEntity> Repository
        {
            get;
        }
        protected readonly IUnitOfWork UnitOfWork;

        protected readonly IMapper Mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.UnitOfWork = unitOfWork;
            this.Mapper = mapper;
        }

        public Task<IEnumerable<TDto>> GetAllAsync()
        {
            return this.Repository.GetAllAsync()
                .ContinueWith(t => this.Mapper.Map<IEnumerable<TDto>>(t.Result));
        }

        public Task<TDto> GetByIdAsync(int id)
        {
            return this.Repository.GetByIdAsync(id)
                .ContinueWith(t => this.Mapper.Map<TDto>(t.Result));
        }

        public virtual async Task AddAsync(TDto model)
        {
            this.Validate(model);

            var entity = this.Mapper.Map<TEntity>(model);

            await this.Repository.AddAsync(entity)
                .ContinueWith(t => this.UnitOfWork.SaveAsync());
        }

        public virtual async Task UpdateAsync(TDto model)
        {
            this.Validate(model);

            var entity = this.Mapper.Map<TEntity>(model);

            this.Repository.Update(entity);
            await this.UnitOfWork.SaveAsync();
        }

        public virtual async Task DeleteAsync(int modelId)
        {
            await this.Repository.DeleteByIdAsync(modelId)
                .ContinueWith(t => this.UnitOfWork.SaveAsync());
        }

        protected abstract void Validate(TDto model);
    }
}
