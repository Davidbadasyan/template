namespace Common.Application.Middlewares;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IHostEnvironment _environment;
    public ErrorHandlerMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case DomainException e:
                    _logger.LogWarning(e.Message);
                    await InvokeException(
                        context,
                        HttpStatusCode.BadRequest,
                        ResponseModel.Create(ResponseType.Info, e.Message));
                    break;
                case KeyNotFoundException e:
                    _logger.LogWarning(e.Message);
                    await InvokeException(
                        context,
                        HttpStatusCode.BadRequest,
                        ResponseModel.Create(ResponseType.Warning, e.Message));
                    break;
                default:
                    _logger.LogError(error, error.Message);
                    await InvokeException(
                        context,
                        HttpStatusCode.InternalServerError,
                        ResponseModel.Create(ResponseType.Error, _environment.IsDevelopment() ? error.Message : "Server error"));
                    break;
            }
        }
    }
    private static async Task InvokeException(HttpContext context, HttpStatusCode statusCode, ResponseModel response)
    {
        context.Response.StatusCode = (int)statusCode;
        var jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
