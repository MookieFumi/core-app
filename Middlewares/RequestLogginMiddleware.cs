using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CoreApp.Api.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            var watch = Stopwatch.StartNew();
            await _next.Invoke(context);
            watch.Stop();

            var logTemplate = @"
            Client IP: {clientIP}
            Request path: {requestPath}
            Request content type: {requestContentType}
            Request content length: {requestContentLength}
            Start time: {startTime}
            Duration: {duration}";

            _logger.LogInformation(logTemplate,
                context.Connection.RemoteIpAddress.ToString(),
                context.Request.Path,
                context.Request.ContentType,
                context.Request.ContentLength,
                startTime,
                watch.ElapsedMilliseconds);
        }
    }
}