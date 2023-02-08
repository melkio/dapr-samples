namespace Backend;

public class CreateTaskCommand
{
    public Guid TaskId { get; set; }
    public string Description { get; set; }
    public string Owner { get; set; }
    public DateTime DueDate { get; set; }
}