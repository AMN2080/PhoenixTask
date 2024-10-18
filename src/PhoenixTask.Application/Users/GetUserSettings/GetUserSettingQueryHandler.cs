using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Users.GetUserSettings;

internal sealed class GetUserSettingQueryHandler(
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider,
    ISettingRepository settingRepository)
    : IQueryHandler<GetUserSettingQuery, IEnumerable<SettingModel>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly ISettingRepository _settingRepository = settingRepository;

    public async Task<IEnumerable<SettingModel>> Handle(GetUserSettingQuery request, CancellationToken cancellationToken)
    {
        var maybeUser = await _userRepository.GetByIdAsync(_userIdentifierProvider.UserId);
        if (maybeUser.HasNoValue)
        {
            return [];
        }

        var settings =await _settingRepository.GetSettingsAsync(maybeUser.Value);

        return settings.Select(x => new SettingModel { Value = x.Value, Key = x.Key.Value }).ToList();
    }
}
