using System.Reflection;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Asset> Assets => Set<Asset>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();

    public DbSet<AssetStatus> AssetStatuses => Set<AssetStatus>();

    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<ReturningRequest> ReturningRequests => Set<ReturningRequest>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<ApplicationUser>()
            .HasQueryFilter(x => x.IsDelete == false);
        builder.Entity<Asset>()
            .HasQueryFilter(x => x.IsDelete == false);
        builder.Entity<ReturningRequest>()
        .HasQueryFilter(x => x.IsDelete == false);
        builder.Entity<Assignment>()
            .HasQueryFilter(x => x.State != AssignmentState.Declined && x.IsDelete == false);

    }
}