using AiDrivenSystem.Infrastructure;

namespace AiDrivenSystem.APIs;

public class CardStacksService : CardStacksServiceBase
{
    public CardStacksService(AiDrivenSystemDbContext context)
        : base(context) { }
}
