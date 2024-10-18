using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Domain.Users;

public interface ISettingRepository
{
    void Insert(Setting setting);
    void Update(Setting setting);
    Task<IEnumerable<Setting>> GetSettingsAsync(User user);
    Task<Maybe<Setting>> GetSettingAsync(User user,Key key);
}
