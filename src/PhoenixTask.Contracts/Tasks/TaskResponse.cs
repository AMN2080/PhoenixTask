namespace PhoenixTask.Contracts.Tasks;

public record TaskResponse(Guid Id, string Name, string Description, DateTime DeadLine, int Order, int Priority);
