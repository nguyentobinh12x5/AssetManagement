using System.Dynamic;

using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Asset> Assets { get; }
    DbSet<Category> Categories { get; }
    DbSet<AssetStatus> AssetStatuses { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}