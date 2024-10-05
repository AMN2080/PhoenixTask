using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Domain.Users;

public interface IUserSettingRepository
{
    Task<Maybe<UserSetting>> GetUserSettingByKeyAsync(Guid userId, Key key);
    Task<IEnumerable<UserSetting>> GetUserSettingsAsync(Guid userId);
    void Insert(UserSetting userSetting);
    void Update(UserSetting userSetting);
    void Delete(UserSetting userSetting);
}
