/*
 * Korotko
 * Domain Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

namespace Korotko.Domain.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string? message) : base(message)
        {

        }
    }
}
