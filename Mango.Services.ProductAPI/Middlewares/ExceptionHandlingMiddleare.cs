using Mango.Services.ProductAPI.Models.Dto;
using System.Net;

namespace Mango.Services.ProductAPI.Middlewares
{
    public class ExceptionHandlingMiddleare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleare> _logger;

        public ExceptionHandlingMiddleare(RequestDelegate next, ILogger<ExceptionHandlingMiddleare> logger)
        {
            _next = next;
            _logger = logger;   
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            //catch (KeyNotFoundException ex)
            //{
            //    await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.NotFound, "Not Found");
            //}
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message,
                            HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        public async Task HandleExceptionAsync(HttpContext context,
            string exMss,HttpStatusCode code,string message)
        {
            _logger.LogError(exMss);
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            ErrorDto dto = new()
            {
                StatusCode = (int)code,
                Message = message
            };
            await response.WriteAsJsonAsync(dto);
        }
    }
}
