/*
 * Korotko
 * Application Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Application.Services;
using Korotko.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Sqids;

namespace Korotko.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, 
            string? dbConnectionStr,
            Version? dbVersion,
            string? cacheServConnectionStr,
            string shortIdAlphabet, int shortIdMinLength)
        {
            services.AddInfrastructure(dbConnectionStr, dbVersion);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheServConnectionStr;
                options.InstanceName = "Korotko";
            });

            services.AddSingleton(new SqidsEncoder<int>(new()
            {
                Alphabet = shortIdAlphabet,
                MinLength = shortIdMinLength
            }));
            services.AddSingleton<IIdGeneratorService, IdGeneratorService>();

            services.AddScoped<IShortLinkService, ShortLinkService>();

            return services;
        }
    }
}
