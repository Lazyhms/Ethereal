// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.Filters
{
    /// <summary>
    /// Implement for <see cref="SchemaModelIgnoreFilter"/> 
    /// </summary>
    public sealed class SchemaModelIgnoreFilter : ISchemaFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!schema.Properties.Any())
            {
                return;
            }

            //var readOnly = schema.Properties.Where(s => s.Value.ReadOnly).Select(s => s.Key);

            //foreach (var item in readOnly)
            //{
            //    schema.Properties.Remove(item);
            //}
        }
    }
}
