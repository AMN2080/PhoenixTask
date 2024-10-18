using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Users.SetSetting;

public sealed record SetSettingCommand(string Key, string Value) : ICommand<Result>;
