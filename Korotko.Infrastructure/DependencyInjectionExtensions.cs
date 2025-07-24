/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Korotko.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            string? dbConnectionStr, Version? dbVersion)
        {
            if (dbVersion == null)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(dbConnectionStr,
                    ServerVersion.AutoDetect(dbConnectionStr)));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(dbConnectionStr,
                    new MySqlServerVersion(dbVersion)));
            }
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
