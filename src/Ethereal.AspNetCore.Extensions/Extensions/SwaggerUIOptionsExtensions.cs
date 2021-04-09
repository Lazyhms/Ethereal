// Copyright (c) Ethereal. All rights reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// SwaggerUIBuilderExtensions
    /// </summary>
    public static class SwaggerUIOptionsExtensions
    {
        /// <summary>
        /// SwaggerEndpoint
        /// </summary>
        public static SwaggerUIOptions SwaggerEndpoint(this SwaggerUIOptions options, IDictionary<string, OpenApiInfo> docs)
        {
            options.ConfigObject.Urls = docs.Select(d => new UrlDescriptor { Name = d.Key, Url = $"/swagger/{d.Key}/swagger.json" });
            return options;
        }

        /// <summary>
        /// UseSwaggerUI
        /// </summary>
        public static SwaggerUIOptions SwaggerEndpoint(this SwaggerUIOptions options, IConfiguration configuration)
        {
            var docs = configuration.Bind<Dictionary<string, OpenApiInfo>>();
            options.ConfigObject.Urls = docs.Select(d => new UrlDescriptor { Name = d.Key, Url = $"/swagger/{d.Key}/swagger.json" });
            return options;
        }

        /// <summary>
        /// UseSwaggerUI
        /// </summary>
        public static SwaggerUIOptions SwaggerEndpoint(this SwaggerUIOptions options, IConfiguration configuration, string key)
        {
            var docs = configuration.Bind<Dictionary<string, OpenApiInfo>>(key);
            options.ConfigObject.Urls = docs.Select(d => new UrlDescriptor { Name = d.Key, Url = $"/swagger/{d.Key}/swagger.json" });
            return options;
        }
    }
}