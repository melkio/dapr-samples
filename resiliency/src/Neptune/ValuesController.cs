using Microsoft.AspNetCore.Mvc;

namespace Neptune;

[Route("api/values")]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var random = new Random(DateTime.Now.Millisecond);
        var value = random.Next(1, 10);

        return Ok(new { Value = value });
    }

    [HttpGet("timeout")]
    public async Task<ActionResult> Timeout()
    {
        var delay = int.Parse(Environment.GetEnvironmentVariable("API_TIMEOUT_VALUE") ?? "200");
        await Task.Delay(delay);

        var random = new Random(DateTime.Now.Millisecond);
        var value = random.Next(1, 10);

        return Ok(new { Value = value });
    }

    [HttpGet("retry")]
    public ActionResult Retry()
    {
        var threshold = int.Parse(Environment.GetEnvironmentVariable("API_RETRY_THRESHOLD") ?? "950");

        var random = new Random(DateTime.Now.Millisecond);
        var value = random.Next(1, 1000);
        if (value >= threshold)
            throw new InvalidOperationException("Transient failure");

        return Ok(new { Value = value });
    }
}
