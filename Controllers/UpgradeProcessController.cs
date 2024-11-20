using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;

[Route("api/[controller]")]
[ApiController]
public class ProcessController : ControllerBase
{
    private readonly IHubContext<ProgressHub> _hubContext;

    public ProcessController(IHubContext<ProgressHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("start-process")]
    public async Task<IActionResult> StartProcess()
    {
        for (int i = 0; i <= 100; i += 10)
        {

            await Task.Delay(500);

          
            await _hubContext.Clients.All.SendAsync("ReceiveProgressUpdate", i);
        }
        return Ok("Process completed");
    }
}
