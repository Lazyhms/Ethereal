// Copyright (c) Ethereal. All rights reserved.

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// s
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class StrongPasswordAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrongPasswordAttribute"/> class.
        /// </summary>
        public StrongPasswordAttribute() : base(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{8,10}$")
        {
        }
    }
}
