using AiDrivenSystem.Infrastructure;

namespace AiDrivenSystem.APIs;

public class UserControlsService : UserControlsServiceBase
{
    public UserControlsService(AiDrivenSystemDbContext context)
        : base(context) { }
}
