/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Domain.Entities;

namespace Korotko.Infrastructure.Repositories
{
    public interface ILinkRepository : IRepository<Link>
    {
        public Task<Link?> GetByDisplayIdAsync(string displayId);
    }
}
