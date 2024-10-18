using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Maybe;

namespace PhoenixTask.Application.Users.GetSetting;

public sealed record GetSettingQuery(string Key) : IQuery<Maybe<SettingModel>>;
