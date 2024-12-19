using AiDrivenSystem.Infrastructure;

namespace AiDrivenSystem.APIs;

public class AiActionsService : AiActionsServiceBase
{
    public AiActionsService(AiDrivenSystemDbContext context)
        : base(context) { }
}
