using Microsoft.EntityFrameworkCore;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Application.Abstractions.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity;

    void Insert<TEntity>(TEntity entity)
           where TEntity : Entity;
    Task<Maybe<TEntity>> GetBydIdAsync<TEntity>(Guid id)
            where TEntity : Entity;
    void Remove<TEntity>(TEntity entity)
            where TEntity : Entity;
}
