// Copyright (c) Ethereal. All rights reserved.

using Ethereal.RateLimit.Infrastructure;

namespace Ethereal.RateLimit
{
    /// <summary>
    /// RateLimitOption
    /// </summary>
    public class RateLimitOptions
    {
        /// <summary>
        /// EnableRateLimiting
        /// </summary>
        public bool EnableRateLimiting { get; set; } = true;

        /// <summary>
        /// WhiteRouters
        /// </summary>
        public IList<string> WhiteRoutes { get; set; } = new List<string>();

        /// <summary>
        /// WhiteRouters
        /// </summary>
        public IList<string> BlackRoutes { get; set; } = new List<string>();

        /// <summary>
        /// RateLimits
        /// </summary>
        public IList<IRateLimit> RateLimits { get; } = new List<IRateLimit>();
    }
}
