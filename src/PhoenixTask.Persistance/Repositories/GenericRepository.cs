using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Persistance.Repositories;

internal abstract class GenericRepository<TEntity>
       where TEntity : Entity
{
    protected GenericRepository(IDbContext dbContext) => DbContext = dbContext;

    protected IDbContext DbContext { get; }

    public void Insert(TEntity entity) => DbContext.Insert(entity);

    public async Task<Maybe<TEntity>> GetByIdAsync(Guid id) => await DbContext.GetBydIdAsync<TEntity>(id);

    public void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity) => DbContext.Remove(entity);
}