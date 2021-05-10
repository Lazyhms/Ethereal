// Copyright (c) Ethereal. All rights reserved.

using Ethereal.AspNetCore;
using Ethereal.AspNetCore.SwaggerGen.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// SwaggerGenOptionsExtensions
    /// </summary>
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// AddJwtGlobalSecurityRequirement
        /// </summary>
        public static SwaggerGenOptions AddJwtGlobalSecurityRequirement(this SwaggerGenOptions options)
        {
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JwtBearer"}
                     },
                     Array.Empty<string>()
                 }
             });
            return options;
        }

        /// <summary>
        /// AddJwtSecurityDefinition
        /// </summary>
        public static SwaggerGenOptions AddJwtSecurityDefinition(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
            {
                Description = CoreStrings.OpenApiSecurityScheme_Description,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "JwtBearer"
            });
            return options;
        }

        /// <summary>
        /// AddJwtSecurityRequirement
        /// </summary>
        public static SwaggerGenOptions AddJwtSecurityRequirement(this SwaggerGenOptions options)
        {
            options.OperationFilter<JwtSecurityOperationFilter>();
            return options;
        }

        /// <summary>
        /// DocInclusionGroupName
        /// </summary>
        public static SwaggerGenOptions DocInclusionGroupName(this SwaggerGenOptions options, string defaultGroupName)
        {
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (string.IsNullOrEmpty(apiDesc.GroupName))
                {
                    apiDesc.GroupName = defaultGroupName;
                }
                return docName.Equals(apiDesc.GroupName);
            });
            return options;
        }

        /// <summary>
        /// Default IncludeXmlComments
        /// </summary>
        public static SwaggerGenOptions IncludeXmlComments(this SwaggerGenOptions options, string? direcotoryPath = default, SearchOption? searchOption = default)
        {
            var xmlFiles = Directory.GetFiles(string.IsNullOrEmpty(direcotoryPath) ? AppContext.BaseDirectory : direcotoryPath, "*.xml", searchOption ?? SearchOption.TopDirectoryOnly);
            Array.ForEach(xmlFiles, xml =>
            {
                options.IncludeXmlComments(xml, true);
            });
            return options;
        }

        /// <summary>
        /// SwaggerDoc
        /// </summary>
        public static SwaggerGenOptions SwaggerDoc(this SwaggerGenOptions options, IDictionary<string, OpenApiInfo> docs)
        {
            options.SwaggerGeneratorOptions.SwaggerDocs = docs;
            return options;
        }

        /// <summary>
        /// SwaggerDoc
        /// </summary>
        public static SwaggerGenOptions SwaggerDoc(this SwaggerGenOptions options, IConfiguration configuration)
        {
            var docs = configuration.Bind<Dictionary<string, OpenApiInfo>>();
            options.SwaggerGeneratorOptions.SwaggerDocs = docs;
            return options;
        }

        /// <summary>
        /// SwaggerDoc
        /// </summary>
        public static SwaggerGenOptions SwaggerDoc(this SwaggerGenOptions options, IConfiguration configuration, string key = "SwaggerDoc")
        {
            var docs = configuration.Bind<Dictionary<string, OpenApiInfo>>(key);
            options.SwaggerGeneratorOptions.SwaggerDocs = docs;
            return options;
        }
    }
}