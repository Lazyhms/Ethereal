// Copyright (c) Ethereal. All rights reserved.

using Ethereal.RateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// s
    /// </summary>
    public static class RateLimitBuilderExtensions
    {

        /// <summary>
        /// Register the RateLimit middleware with provided options
        /// </summary>
        public static IApplicationBuilder UseRateLimit(this IApplicationBuilder app, RateLimitOptions options)
        {
            var jsonSerializerOptions = app.ApplicationServices.GetService<IOptions<JsonOptions>>()?.Value?.JsonSerializerOptions;
            return app.UseMiddleware<RateLimitMiddleware>(options, jsonSerializerOptions);
        }

        /// <summary>
        /// Register the RateLimit middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UseRateLimit(this IApplicationBuilder app, Action<RateLimitOptions>? setupAction = default)
        {
            var value = new RateLimitOptions();
            setupAction?.Invoke(value);
            return app.UseRateLimit(value);
        }

    }
}
