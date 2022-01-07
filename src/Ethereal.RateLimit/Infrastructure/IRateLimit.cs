// Copyright (c) Ethereal. All rights reserved.

namespace Ethereal.RateLimit.Infrastructure
{
    /// <summary>
    /// IRateLimit
    /// </summary>
    public interface IRateLimit
    {
        /// <summary>
        /// ClientId
        /// </summary>
        Guid ClientId { get; set; }

        /// <summary>
        /// The name of the rule.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The number of seconds locked after triggering rate limiting. 0 means not locked
        /// </summary>
        public int LockSeconds { get; set; }

        /// <summary>
        /// EnableRateLimiting
        /// </summary>
        bool EnableRateLimiting { get; set; }

    }
}
