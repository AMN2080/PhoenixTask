namespace PhoenixTask.Contracts.Tasks;

public class TaskModel
{
    public string Name { get; set; }
    public Guid BoardId { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
    public int Order { get; set; }
    public int Priority { get; set; }
}
