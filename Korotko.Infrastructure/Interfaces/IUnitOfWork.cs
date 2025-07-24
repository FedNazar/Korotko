/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Infrastructure.Repositories;

namespace Korotko.Infrastructure
{
    public interface IUnitOfWork
    {
        ILinkRepository LinkRepository { get; }

        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();

        public Task SaveChangesAsync();
    }
}
