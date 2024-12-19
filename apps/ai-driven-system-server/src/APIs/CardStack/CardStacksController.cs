using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[ApiController()]
public class CardStacksController : CardStacksControllerBase
{
    public CardStacksController(ICardStacksService service)
        : base(service) { }
}
