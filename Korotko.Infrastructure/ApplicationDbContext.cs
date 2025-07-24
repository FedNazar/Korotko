/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Korotko.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
