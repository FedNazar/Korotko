/*
 * Korotko
 * Application Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Sqids;

namespace Korotko.Application.Services
{
    public class IdGeneratorService(SqidsEncoder<int> _idEncoder) : IIdGeneratorService
    {
        public string Generate(int sequentialLinkId)
        {
            return _idEncoder.Encode(sequentialLinkId);
        }
    }
}
