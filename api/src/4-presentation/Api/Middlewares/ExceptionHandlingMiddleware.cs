using System.Net;
using Resto.Application.Common.Exceptions;
using Resto.Common.Exceptions;

namespace Resto.Api.Middlewares;

internal sealed class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    )
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex,
            "Error while processing request {Method} {Path}: {Message}",
            context.Request.Method,
            context.Request.Path,
            ex.Message);
        var code = ex switch
        {
            // generic exceptions
            ArgumentException _ => HttpStatusCode.BadRequest,

            // from common project
            BadDataException _ => HttpStatusCode.BadRequest,
            BadRequestException _ => HttpStatusCode.BadRequest,
            DataChangedException _ => HttpStatusCode.Conflict,
            ForbiddenException _ => HttpStatusCode.Forbidden,
            NotFoundException _ => HttpStatusCode.NotFound,
            UnauthorizedException _ => HttpStatusCode.Unauthorized,

            // from domain project
            // ValueObjectException _ => HttpStatusCode.BadRequest,

            // from application project
            MappingException _ => HttpStatusCode.BadRequest,
            // ServiceBusyException _ => HttpStatusCode.ServiceUnavailable, // 503
            ValidationException _ => HttpStatusCode.BadRequest,

            _ => HttpStatusCode.InternalServerError
        };

        var response = new ExceptionResponse { Error = ex.Message };

        if (ex is ValidationException vex)
        {
            response.ValidationErrors = vex.Errors;
        }

        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsJsonAsync(response);
    }
}

// TODO replace with record
internal sealed class ExceptionResponse
{
    public string Error { get; set; }
    public IDictionary<string, string[]> ValidationErrors { get; set; }
}