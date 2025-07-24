/*
 * Korotko
 * Application Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

namespace Korotko.Application.Services
{
    public interface IShortLinkService
    {
        public Task<string> CreateAsync(string url);
        public Task<string> GetAsync(string shortId);
    }
}
