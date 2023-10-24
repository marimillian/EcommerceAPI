using Newtonsoft.Json;
using System.Net;
using UsuariosApi.Response.Error;

namespace UsuariosApi.Middlewares
{
    public class ErrorMiddleware
    {
        private RequestDelegate _next;
      
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;           

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (UsuarioExceptions e)
            {

                await ErroNameException(context, e);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private Task ErroNameException(HttpContext context, UsuarioExceptions ex)
        {
            ErrorResponse errorResponse;

            

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
}
