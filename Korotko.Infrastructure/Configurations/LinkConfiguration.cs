/*
 * Korotko
 * Infrastructure Layer
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Korotko.Infrastructure.Configurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .UseMySqlIdentityColumn();

            builder.Property(x => x.Url)
                   .IsRequired();

            builder.HasIndex(x => x.DisplayId);
        }
    }
}
