using AiDrivenSystem.Infrastructure;

namespace AiDrivenSystem.APIs;

public class NodesService : NodesServiceBase
{
    public NodesService(AiDrivenSystemDbContext context)
        : base(context) { }
}
