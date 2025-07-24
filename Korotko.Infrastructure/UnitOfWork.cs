/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Korotko.Infrastructure
{
    public class UnitOfWork(ApplicationDbContext _dbContext) : IUnitOfWork
    {
        public ILinkRepository LinkRepository { get; } = 
            new LinkRepository(_dbContext);

        private IDbContextTransaction? _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
