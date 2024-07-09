using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Asset> Assets { get; }
    DbSet<Category> Categories { get; }
    DbSet<AssetStatus> AssetStatuses { get; }
    DbSet<Assignment> Assignments { get; }
    DbSet<ReturningRequest> ReturningRequests { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}