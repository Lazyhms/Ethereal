// Copyright (c) Ethereal. All rights reserved.

namespace Basket.API.Infrastructure.Filters
{
    /// <summary>
    /// Error response
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// Error messages
        /// </summary>
        public string?[]? Messages { get; set; }

        /// <summary>
        /// Error developer message
        /// </summary>
        public object? DeveloperMessage { get; set; }
    }
}
