// Copyright (c) Ethereal. All rights reserved.
//

using Ethereal.NETCore;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class RequisiteAttribute : RequiredAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequisiteAttribute"/> class.
        /// </summary>
        public RequisiteAttribute() 
            => ErrorMessage = CoreStrings.RequiredAttribute_Invalid;
    }
}
