namespace Dotnet9.EShop.Service.Catalog.Infrastructure.Middleware;

public class LoggingEventMiddleware<TEvent> : EventMiddleware<TEvent> where TEvent : IEvent
{
    private readonly ILogger<LoggingEventMiddleware<TEvent>> _logger;

    public LoggingEventMiddleware(ILogger<LoggingEventMiddleware<TEvent>> logger)
    {
        _logger = logger;
    }

    public override async Task HandleAsync(TEvent @event, EventHandlerDelegate next)
    {
        _logger.LogInformation("---- Handling command {CommandName} {(@Command)}",
            @event.GetType().GetGenericTypeName(), @event);
        await next();
    }
}