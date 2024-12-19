using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[ApiController()]
public class AiActionsController : AiActionsControllerBase
{
    public AiActionsController(IAiActionsService service)
        : base(service) { }
}
