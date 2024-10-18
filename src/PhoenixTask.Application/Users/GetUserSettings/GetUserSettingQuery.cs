using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Users;

namespace PhoenixTask.Application.Users.GetUserSettings;

public sealed record GetUserSettingQuery : IQuery<IEnumerable<SettingModel>>;
