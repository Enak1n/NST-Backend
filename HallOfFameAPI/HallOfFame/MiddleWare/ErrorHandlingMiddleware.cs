using HallOfFame.Utilities.Interfaces;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerManager _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILoggerManager logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
