using AssetManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagement.Infrastructure.Data.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(t => t.Location)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(t => t.Specification)
            .HasMaxLength(1200)
            .IsRequired();

    }
}