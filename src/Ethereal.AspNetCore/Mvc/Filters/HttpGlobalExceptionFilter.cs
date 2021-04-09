// Copyright (c) Ethereal. All rights reserved.

using Ethereal.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// HttpGlobalExceptionFilter
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGlobalExceptionFilter"/> class.
        /// </summary>
        public HttpGlobalExceptionFilter(
            IWebHostEnvironment env,
            ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            var json = new JsonErrorResponse
            {
                Messages = new[] { CoreStrings.Global_Exception }
            };
            if (_env.IsDevelopment())
            {
                json.DeveloperMessage = context.Exception;
            }

            context.Result = new ObjectResult(json) { StatusCode = StatusCodes.Status500InternalServerError };
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public object? DeveloperMessage { get; set; }
            public string[]? Messages { get; set; }
        }
    }
}