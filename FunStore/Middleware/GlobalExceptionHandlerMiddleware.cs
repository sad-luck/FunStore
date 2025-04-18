using FunStore.ValidationExceptions;

namespace FunStore.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string message;

        switch (exception)
        {
            case UserNotFoundException:
                statusCode = StatusCodes.Status401Unauthorized;
                message = exception.Message;
                break;
            case ValidationException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = exception.Message;
                break;
        }

        context.Response.StatusCode = statusCode;

        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            error = message
        });

        return context.Response.WriteAsync(result);
    }
}