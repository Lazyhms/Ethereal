// Copyright (c) Ethereal. All rights reserved.

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validates a phonenumber.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MobilePhoneAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobilePhoneAttribute"/> class.
        /// </summary>
        public MobilePhoneAttribute() : base("/^[1][3,5,7,8][0-9]{9}$/")
        {
        }
    }
}
