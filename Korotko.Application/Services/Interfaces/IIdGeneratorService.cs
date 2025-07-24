/*
 * Korotko
 * Application Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

namespace Korotko.Application.Services
{
    public interface IIdGeneratorService
    {
        public string Generate(int sequentialLinkId);
    }
}
