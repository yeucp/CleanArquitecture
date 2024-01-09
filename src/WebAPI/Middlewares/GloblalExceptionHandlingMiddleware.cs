using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class GloblalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GloblalExceptionHandlingMiddleware> _logger;

        public GloblalExceptionHandlingMiddleware(ILogger<GloblalExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try {
                await next(context);
            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var statusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.StatusCode = statusCode;

                ProblemDetails problemDetails = new() { 
                    Status = statusCode,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal error has ocurred"
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}
