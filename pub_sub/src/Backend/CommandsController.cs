using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Backend;

[Route("commands")]
public class CommandsController : ControllerBase
{
    private readonly ILogger<CommandsController> logger;

    public CommandsController(ILogger<CommandsController> logger)
    {
        this.logger = logger;
    }

    [HttpPost("tasks")]
    [Topic("pubsub1", "tasks")]
    public ActionResult Handle([FromBody] CreateTaskCommand command)
    {
        logger.LogInformation("Handling CreateTaskCommand#{id}", command.TaskId);
        return Ok();
    }
}
