// Copyright (c) Ethereal. All rights reserved.

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

/// <summary>
/// 1
/// </summary>
public class ValidationMetadataProvider : IValidationMetadataProvider
{
    /// <summary>
    /// 2
    /// </summary>
    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        var attribute = (RequiredAttribute?)context.PropertyAttributes?.FirstOrDefault(f => f is RequiredAttribute);
        if (attribute is not null)
        {
            attribute.ErrorMessage = "{0}不能为空";
        }

    }
}
