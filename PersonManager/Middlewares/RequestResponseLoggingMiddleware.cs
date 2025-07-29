using PersonManager.Domain.Entities;
using PersonManager.Domain.Repositories;
using System.Text;

namespace PersonManager.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestResponseLogRepository logRepository)
        {
            context.Request.EnableBuffering();

            // Read request body
            string requestBody = "";
            if (context.Request.ContentLength > 0)
            {
                context.Request.Body.Position = 0;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
            }

            // Capture request headers
            var requestHeaders = System.Text.Json.JsonSerializer.Serialize(context.Request.Headers);

            // Capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Capture response headers
            var responseHeaders = System.Text.Json.JsonSerializer.Serialize(context.Response.Headers);

            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            // Save request log
            var requestLog = new RequestResponseLog
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                RequestBody = requestBody,
                ResponseBody = null,
                StatusCode = context.Response.StatusCode,
                Timestamp = DateTime.UtcNow,
                IpAddress = ipAddress,
                LogType = "Request",
                RequestHeaders = requestHeaders,
                ResponseHeaders = null
            };
            await logRepository.AddAsync(requestLog);

            // Save response log
            var responseLog = new RequestResponseLog
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                RequestBody = null,
                ResponseBody = responseBodyText,
                StatusCode = context.Response.StatusCode,
                Timestamp = DateTime.UtcNow,
                IpAddress = ipAddress,
                LogType = "Response",
                RequestHeaders = null,
                ResponseHeaders = responseHeaders
            };
            await logRepository.AddAsync(responseLog);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}