using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Frontend;

[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly DaprClient client;
    
    public TasksController(DaprClient client)
    {
        this.client = client;    
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PostTaskModel model)
    {
        var command = new CreateTaskCommand
        {
            TaskId = Guid.NewGuid(),
            Description = model.Description,
            Owner = model.Owner,
            DueDate = model.DueDate
        };
        await client
            .PublishEventAsync("pubsub1", "tasks", command)
            .ConfigureAwait(false);

        return Accepted(new { Id = command.TaskId });
    }
}
