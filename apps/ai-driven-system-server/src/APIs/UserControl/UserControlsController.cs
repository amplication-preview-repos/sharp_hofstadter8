using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[ApiController()]
public class UserControlsController : UserControlsControllerBase
{
    public UserControlsController(IUserControlsService service)
        : base(service) { }
}
