using Ecommerce_API.Response.Error;
using Newtonsoft.Json;
using System.Net;

namespace Ecommerce_API.Middlewares;

public class ErrorMiddleware
{
    private RequestDelegate _next;
    private ILogger<ErrorMiddleware> _logger;
    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
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

        catch (NameExceptions e)
        {
            
            await ErroNameException(context, e);
        }

        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        
    }   

    private Task ErroNameException(HttpContext context, NameExceptions ex) 
    {
        ErrorResponse errorResponse;

        _logger.LogError($"Ocorreu um erro: {ex}");

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
        {
           
            errorResponse = new ErrorResponse(HttpStatusCode.BadRequest.ToString(),
                $"{ex.Message} {ex?.InnerException?.Message}");
        }

        else
        {
            
            errorResponse = new ErrorResponse(HttpStatusCode.BadRequest.ToString(),
                "Ocorreu um erro interno. Por favor, verifique com o desenvolvedor.");
        }

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var result = JsonConvert.SerializeObject(errorResponse);
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        ErrorResponse errorResponse;

        _logger.LogError($"Ocorreu um erro: {ex}");

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
        {
            errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(), 
                $"{ex.Message} {ex?.InnerException?.Message}");
        }

        else
        {
            errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(), 
                "Ocorreu um erro interno. Por favor, verifique com o desenvolvedor.");
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(errorResponse);
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }
}
