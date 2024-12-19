using AiDrivenSystem.APIs;

namespace AiDrivenSystem;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAiActionsService, AiActionsService>();
        services.AddScoped<ICardStacksService, CardStacksService>();
        services.AddScoped<IEdgesService, EdgesService>();
        services.AddScoped<INodesService, NodesService>();
        services.AddScoped<IUserControlsService, UserControlsService>();
    }
}
