using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace ArtilleryApi.Middleware;

public class RequestTrackingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTrackingMiddleware> _logger;
    private static ConcurrentDictionary<long, int> _requestCounts = new ConcurrentDictionary<long, int>();
    private static Timer _timer;

    public RequestTrackingMiddleware(RequestDelegate next, ILogger<RequestTrackingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _timer = new Timer(LogRequestCounts, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var currentSecond = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        _requestCounts.AddOrUpdate(currentSecond, 1, (key, count) => count + 1);

        await _next(context);
    }

    private void LogRequestCounts(object state)
    {
        var previousSecond = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 1;
        if (_requestCounts.TryRemove(previousSecond, out int count))
        {
            _logger.LogInformation($"Requests in the last second: {count}");
        }
    }
}