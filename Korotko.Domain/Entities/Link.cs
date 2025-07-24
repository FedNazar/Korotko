/*
 * Korotko
 * Domain Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Korotko.Domain.Entities
{
    public class Link : BaseEntity
    {
        private static readonly string URL_REGEX = "^https?:\\/\\/.+[.].+$";

        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (!ValidUrl(value)) throw new InvalidUrlException("URL is invalid.");

                _url = value;
            }
        }

        public string? DisplayId { get; set; }

        public Link(string url)
        {
            Url = url;
        }

        public static bool ValidUrl(string url)
        {
            return Regex.Match(url, URL_REGEX) != null;
        }
    }
}
