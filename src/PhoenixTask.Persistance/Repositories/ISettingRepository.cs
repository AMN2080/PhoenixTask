using Microsoft.EntityFrameworkCore;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Persistance.Repositories;

internal sealed class SettingRepository(IDbContext dbContext) : GenericRepository<Setting>(dbContext), ISettingRepository
{
    public async Task<Maybe<Setting>> GetSettingAsync(User user, Key key) 
        => Maybe<Setting>.From(await DbContext.Set<Setting>().SingleOrDefaultAsync(c => c.UserId == user.Id && c.Key.Value == key.Value));

    public async Task<IEnumerable<Setting>> GetSettingsAsync(User user) 
        => await DbContext.Set<Setting>().Where(x => x.UserId == user.Id).ToListAsync();
}
