/*
 * Korotko
 * Application Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Application.Exceptions;
using Korotko.Application.Services;
using Korotko.Domain.Entities;
using Korotko.Infrastructure;
using Microsoft.Extensions.Caching.Distributed;

namespace Korotko.Application.Services
{
    public class ShortLinkService(IUnitOfWork _unitOfWork, 
        IIdGeneratorService _idGenService,
        IDistributedCache _cache) : IShortLinkService
    {
        public async Task<string> GetAsync(string shortId)
        {
            string? cachedUrl = await _cache.GetStringAsync(shortId);
            if (cachedUrl != null) return cachedUrl;

            Link? link = await _unitOfWork.
                LinkRepository.GetByDisplayIdAsync(shortId);

            if (link == null) 
                throw new LinkNotFoundException("Link was not found.");

            await _cache.SetStringAsync(shortId, link.Url);

            return link.Url;
        }

        public async Task<string> CreateAsync(string url)
        {
            await _unitOfWork.BeginTransactionAsync();

            Link link = new(url);
            await _unitOfWork.LinkRepository.CreateAsync(link);
            await _unitOfWork.SaveChangesAsync();

            string displayId = _idGenService.Generate(link.Id);
            link.DisplayId = displayId;
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            return displayId;
        }
    }
}
