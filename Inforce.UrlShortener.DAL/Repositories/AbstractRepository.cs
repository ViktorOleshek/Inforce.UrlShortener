﻿using Inforce.UrlShortener.DAL.Data;
using Inforce.UrlShortener.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inforce.UrlShortener.DAL.Repositories
{
    public abstract class AbstractRepository<TBaseEntity>
        where TBaseEntity : BaseEntity
    {
        protected AbstractRepository(UrlShortenerContext context)
        {
            this.Context = context;
        }

        protected UrlShortenerContext Context { get; }

        public async Task<IEnumerable<TBaseEntity>> GetAllAsync()
        {
            return await this.Context.Set<TBaseEntity>().ToListAsync();
        }

        public Task<TBaseEntity> GetByIdAsync(int id) =>
            this.Context.Set<TBaseEntity>().FirstOrDefaultAsync(x => x.Id == id);

        public Task AddAsync(TBaseEntity entity) =>
            this.Context.AddAsync(entity).AsTask();

        public void Delete(TBaseEntity entity) =>
            this.Context.Remove(entity);

        public virtual async Task DeleteByIdAsync(int id)
        {
            if (id > 0)
            {
                var entity = await this.GetByIdAsync(id);
                if (entity != null)
                {
                    this.Delete(entity);
                }
            }
        }

        public void Update(TBaseEntity entity) =>
            this.Context.Update(entity);
    }
}