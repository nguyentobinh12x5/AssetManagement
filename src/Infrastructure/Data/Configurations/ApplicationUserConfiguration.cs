using AssetManagement.Domain.Entities;
using AssetManagement.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetManagement.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(t => t.StaffCode)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(t => t.FirstName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(t => t.LastName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(t => t.Location)
            .HasMaxLength(256)
            .IsRequired();

    }
}