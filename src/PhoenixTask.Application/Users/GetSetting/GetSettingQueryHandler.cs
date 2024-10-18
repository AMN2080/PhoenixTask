using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Users.GetSetting;

internal sealed class GetSettingQueryHandler(
    ISettingRepository settingRepository,
    IUserRepository userRepository,
    IUserIdentifierProvider userIdentifierProvider
    ) : IQueryHandler<GetSettingQuery, Maybe<SettingModel>>
{
    private readonly ISettingRepository _settingRepository = settingRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;

    public async Task<Maybe<SettingModel>> Handle(GetSettingQuery request, CancellationToken cancellationToken)
    {
        var keyResult = Key.Create(request.Key);

        if (keyResult.IsFailure)
        {
            return Maybe<SettingModel>.None;
        }

        var maybeUser = await _userRepository.GetByIdAsync(_userIdentifierProvider.UserId);
        if (maybeUser.HasNoValue)
        {
            return Maybe<SettingModel>.None;
        }

        var maybeSetting = await _settingRepository.GetSettingAsync(maybeUser.Value, keyResult.Value);
        if (maybeSetting.HasNoValue)
        {
            return Maybe<SettingModel>.None;
        }
        return new SettingModel() { Key= maybeSetting.Value.Key.Value,Value = maybeSetting.Value.Value};
    }
}