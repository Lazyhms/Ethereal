// Copyright (c) Ethereal. All rights reserved.

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validates an account.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AccountAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountAttribute"/> class.
        /// </summary>
        public AccountAttribute()
            : base("^[a-zA-Z][a-zA-Z0-9_]{4,15}$")
        {
        }
    }
}
