using Microsoft.EntityFrameworkCore;
using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Application.Abstractions.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity;

    void Insert<TEntity>(TEntity entity)
           where TEntity : Entity;
}
