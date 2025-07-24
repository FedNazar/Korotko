/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Korotko.Infrastructure.Repositories
{
    public class LinkRepository(ApplicationDbContext _dbContext) : ILinkRepository
    {
        public async Task<Link?> GetAsync(int id)
        {
            return await _dbContext.Links.FindAsync(id);
        }

        public async Task<Link?> GetByDisplayIdAsync(string displayId)
        {
            return await _dbContext.Links
                .Where(x => x.DisplayId.Equals(displayId))
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Link entity)
        {
            await _dbContext.Links.AddAsync(entity);
        }
    }
}
