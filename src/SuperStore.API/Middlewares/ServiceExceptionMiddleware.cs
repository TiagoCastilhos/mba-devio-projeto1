using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Application.Exceptions;
using SuperStore.Authorization.Exceptions;

namespace SuperStore.API.Middlewares;
internal sealed class ServiceExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ServiceExceptionMiddleware> _logger;

    public ServiceExceptionMiddleware(RequestDelegate next, ILogger<ServiceExceptionMiddleware> logger)
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
        catch (IdentityException ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
        }
        catch (ServiceApplicationException ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex, ex.StatusCode);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ProblemDetails
        {
            Title = "Ocorreu um erro ao processar a requisição",
            Status = context.Response.StatusCode,
            Type = $"https://httpstatuses.com/{context.Response.StatusCode}",
            Instance = context.Request.Path,
            Detail = exception.Message
        };

        await JsonSerializer.SerializeAsync(context.Response.Body, response);
    }
}
