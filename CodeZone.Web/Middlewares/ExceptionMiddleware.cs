using System.Net;
using System.Text.Json;

namespace CodeZone.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware ( RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync ( HttpContext context )
        {
            try
            {
                await _next ( context );
            }
            catch ( Exception ex )
            {
                _logger.LogError ( ex, "An unhandled exception has occurred: {Message}", ex.Message );

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                object response;

                if ( _env.IsDevelopment ( ) )
                {
                    response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace?.ToString ( )
                    };
                }
                else
                {
                    response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error. Please try again later."
                    };
                }

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize ( response, options );

                await context.Response.WriteAsync ( json );
            }
        }
    }
}