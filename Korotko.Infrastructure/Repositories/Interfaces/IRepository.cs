/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Domain.Entities;

namespace Korotko.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity?> GetAsync(int id);
        public Task CreateAsync(TEntity entity);
    }
}
