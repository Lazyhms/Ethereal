// Copyright (c) Ethereal. All rights reserved.

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// s
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class SpecialPasswordAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakPasswordAttribute"/> class.
        /// </summary>
        public SpecialPasswordAttribute() : base(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$")
        {
        }
    }
}
1