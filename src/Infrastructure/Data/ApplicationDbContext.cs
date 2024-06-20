using System.Reflection;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<ApplicationUser>()
			.HasQueryFilter(x => x.IsDelete == false);
	}
}
