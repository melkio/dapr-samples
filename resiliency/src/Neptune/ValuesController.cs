using Microsoft.AspNetCore.Mvc;

namespace Neptune;

[Route("api/values")]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var delay = int.Parse(Environment.GetEnvironmentVariable("API_DELAY") ?? "200");
        await Task.Delay(delay);
        
        var random = new Random(DateTime.Now.Millisecond);
        var value = random.Next(1, 10);

        return Ok(new { Value = value });
    }
}