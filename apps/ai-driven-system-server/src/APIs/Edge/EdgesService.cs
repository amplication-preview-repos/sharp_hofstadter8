using AiDrivenSystem.Infrastructure;

namespace AiDrivenSystem.APIs;

public class EdgesService : EdgesServiceBase
{
    public EdgesService(AiDrivenSystemDbContext context)
        : base(context) { }
}
