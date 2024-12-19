using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[ApiController()]
public class EdgesController : EdgesControllerBase
{
    public EdgesController(IEdgesService service)
        : base(service) { }
}
