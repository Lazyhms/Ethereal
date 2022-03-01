// Copyright (c) Ethereal. All rights reserved.

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// s
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class WeakPasswordAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakPasswordAttribute"/> class.
        /// </summary>
        public WeakPasswordAttribute() : base(@"^[a-zA-Z]\w{5,17}$")
        {
        }
    }
}
