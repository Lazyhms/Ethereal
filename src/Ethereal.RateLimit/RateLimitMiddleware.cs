// Copyright (c) Ethereal. All rights reserved.

using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Ethereal.RateLimit
{
    /// <summary>
    /// s
    /// </summary>
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimitOptions _options;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimitMiddleware"/> class.
        /// </summary>
        public RateLimitMiddleware(
            RequestDelegate next,
            RateLimitOptions options,
            JsonSerializerOptions jsonSerializerOptions)
        {
            _next = next;
            _options = options;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        public async Task Invoke(HttpContext httpContext)
        {
            if (_options.EnableRateLimiting)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "Too many requests" }, _jsonSerializerOptions));
                return;
            }

            await _next(httpContext);
        }
    }
}
