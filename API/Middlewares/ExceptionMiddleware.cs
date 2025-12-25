using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, "{message}", e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment()
                ? new ApiExceptions(context.Response.StatusCode, e.Message, e.StackTrace)
                : new ApiExceptions(context.Response.StatusCode, e.Message,
                    "An internal error occured while serving your request. Please try later");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
            throw;
        }
    }
}