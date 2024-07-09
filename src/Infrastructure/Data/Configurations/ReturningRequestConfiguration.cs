using AssetManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagement.Infrastructure.Data.Configurations;

public class ReturningRequestConfiguration : IEntityTypeConfiguration<ReturningRequest>
{
    public void Configure(EntityTypeBuilder<ReturningRequest> builder)
    {
        builder.Property(t => t.RequestedBy)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(t => t.AcceptedBy)
            .HasMaxLength(256);
    }
}