using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.Errors;

namespace Shared.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next { get; }
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            object errors = null;
            switch (ex)
            {
                case RestException re:
                    _logger.LogError(ex, "API REST ERROR");
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    _logger.LogError(ex, "API REST Exception");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error en la api" : e.Message;
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                string result = System.Text.Json.JsonSerializer.Serialize(new { Message = "One or more validation errors occurred.", Errors = new[] { errors } });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
