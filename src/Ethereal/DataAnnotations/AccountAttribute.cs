// Copyright (c) Ethereal. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validates an account.
    /// </summary>
    public class AccountAttribute : DataTypeAttribute
    {
        private readonly Regex _accountRegex = new("^[a-zA-Z][a-zA-Z0-9_]{4,15}$");

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountAttribute"/> class.
        /// </summary>
        public AccountAttribute() : base(DataType.Custom)
        {
        }

        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }
            if (value is string str)
            {
                return _accountRegex.IsMatch(str);
            }
            return false;
        }
    }
}
