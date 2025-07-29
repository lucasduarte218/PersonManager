using Microsoft.AspNetCore.Http;
using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;
using System.Net;
using System.Text.Json;

namespace PersonManager.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IExceptionLogRepository exceptionLogRepository)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var log = new ExceptionLog
                {
                    Path = context.Request.Path,
                    Method = context.Request.Method,
                    ExceptionMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    Timestamp = DateTime.UtcNow
                };

                var correlationId = await exceptionLogRepository.AddAsync(log);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Message = "Ocorreu um erro inesperado. Por favor, contate o suporte com o código de correlação.",
                    CorrelationId = correlationId
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}