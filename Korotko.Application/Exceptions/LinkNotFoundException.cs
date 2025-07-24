/*
 * Korotko
 * Application Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

namespace Korotko.Application.Exceptions
{
    public class LinkNotFoundException : Exception
    {
        public LinkNotFoundException(string? message) : base(message)
        {

        }
    }
}
