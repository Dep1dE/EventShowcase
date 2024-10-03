using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Произошла ошибка."); 
            await HandleExceptionAsync(context, ex);  
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;

        if (exception is UnauthorizedAccessException)
            statusCode = HttpStatusCode.Unauthorized;
        else if (exception is ArgumentException)
            statusCode = HttpStatusCode.BadRequest;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        response.StatusCode = (int)statusCode;

        return response.WriteAsync(result);     
    }
}
