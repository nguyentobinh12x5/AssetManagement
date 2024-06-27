using AssetManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagement.Infrastructure.Data.Configurations;

public class AssetStatusConfiguration : IEntityTypeConfiguration<AssetStatus>
{
    public void Configure(EntityTypeBuilder<AssetStatus> builder)
    {
        builder.HasIndex(t => t.Name)
            .IsUnique();
        builder.Property(t => t.Name)
            .HasMaxLength(256)
            .IsRequired();

    }
}