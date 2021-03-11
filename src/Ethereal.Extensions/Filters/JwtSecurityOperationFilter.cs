// Copyright (c) Ethereal. All rights reserved.
//

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace Ethereal.AspNetCore.SwaggerGen.Filters
{
    /// <summary>
    /// JwtSecurityOperationFilter
    /// </summary>
    internal sealed class JwtSecurityOperationFilter : IOperationFilter
    {
        private readonly OpenApiSecurityRequirement OpenApiSecurityRequirement = new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JwtBearer"}
                     },
                     Array.Empty<string>()
                 }
             };

        /// <summary>
        /// Apply
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!Attribute.IsDefined(context.MethodInfo, typeof(AuthorizeAttribute))
                    && !Attribute.IsDefined(context.MethodInfo, typeof(AllowAnonymousAttribute)))
            {
                if (context.MethodInfo.DeclaringType is MemberInfo memberInfo
                       && Attribute.IsDefined(memberInfo, typeof(AuthorizeAttribute)))
                {
                    operation.Security.Add(OpenApiSecurityRequirement);
                }
            }
            else if (Attribute.IsDefined(context.MethodInfo, typeof(AuthorizeAttribute)))
            {
                operation.Security.Add(OpenApiSecurityRequirement);
            }
        }
    }
}
