using System.Net;
using System.Text.Json;
using Core.Shared.Result;

namespace Api.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResult = new ErrorResult("Ocorreu um erro interno no servidor.", context.Response.StatusCode);

        var json = JsonSerializer.Serialize(errorResult);
        await context.Response.WriteAsync(json);
    }
}
