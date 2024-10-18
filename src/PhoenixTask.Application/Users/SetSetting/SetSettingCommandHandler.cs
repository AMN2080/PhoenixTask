using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Users.SetSetting;

internal sealed class SetSettingCommandHandler
    (ISettingRepository settingRepository,
    IUserIdentifierProvider userIdentifierProvider,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SetSettingCommand, Result>
{
    private readonly ISettingRepository _settingRepository = settingRepository;
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(SetSettingCommand request, CancellationToken cancellationToken)
    {
        var keyResult = Key.Create(request.Key);

        if (keyResult.IsFailure)
        {
            return Result.Failure(keyResult.Error);
        }

        var maybeUser = await _userRepository.GetByIdAsync(_userIdentifierProvider.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var maybeSetting = await _settingRepository.GetSettingAsync(maybeUser.Value, keyResult.Value);

        if (maybeSetting.HasNoValue)
        {
            var setting = new Setting() { Key = keyResult.Value, Value = request.Value, UserId = maybeUser.Value.Id };
            _settingRepository.Insert(setting);
        }
        else
        {
            var setting = maybeSetting.Value;
            setting.Value = request.Value;
            _settingRepository.Update(setting);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
