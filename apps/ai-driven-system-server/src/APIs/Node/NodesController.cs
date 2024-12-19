using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[ApiController()]
public class NodesController : NodesControllerBase
{
    public NodesController(INodesService service)
        : base(service) { }
}
