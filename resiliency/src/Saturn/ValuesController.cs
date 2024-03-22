using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Saturn;

// [Route("api/values")]
// public class ValuesController : ControllerBase
// {
//     private readonly IHttpClientFactory factory;
//
//     public ValuesController(IHttpClientFactory factory)
//     {
//         this.factory = factory;
//     }
//
//     [HttpGet]
//     public ActionResult Get()
//     {
//         var random = new Random(DateTime.Now.Millisecond);
//         var seed = random.Next(1, 10);
//
//         return Ok(new { value = seed });
//     }
//
//     [HttpGet("composed")]
//     public async Task<ActionResult> GetDetailed()
//     {
//         var random = new Random(DateTime.Now.Millisecond);
//         var seed = random.Next(1, 10);
//
//         var client = factory.CreateClient("neptune");
//         var response = await client.GetFromJsonAsync<NeptuneResponse>("api/values");
//
//         var model = new { saturn = seed, neptune = response?.Value };
//         return Ok(model);
//     }
// }
//
[Route("api/values")]
public class ValuesController : ControllerBase
{
    private readonly DaprClient client;

    public ValuesController(DaprClient client)
    {
        this.client = client;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var random = new Random(DateTime.Now.Millisecond);
        var seed = random.Next(1, 10);

        return Ok(new { value = seed });
    }

    [HttpGet("timeout")]
    public Task<ActionResult> TimeoutScenario()
        => ExecuteScenario("timeout");

    [HttpGet("retry")]
    public Task<ActionResult> RetryScenario()
        => ExecuteScenario("retry");

    private async Task<ActionResult> ExecuteScenario(string scenario)
    {
        var random = new Random(DateTime.Now.Millisecond);
        var seed = random.Next(1, 10);

        var response = await client.InvokeMethodAsync<NeptuneResponse>(
            HttpMethod.Get,
            "neptune",
            $"api/values/{scenario}"
        );

        var model = new { saturn = seed, neptune = response?.Value };
        return Ok(model);
    }
}
