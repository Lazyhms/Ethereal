// Copyright (c) Ethereal. All rights reserved.

using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validates a password.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordAttribute : DataTypeAttribute
    {
        private readonly Regex _weakRegex = new(@"^[a-zA-Z]\w{5,17}$");
        private readonly Regex _specialRegex = new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$");
        private readonly Regex _strongRegex = new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{8,10}$");

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordAttribute"/> class.
        /// </summary>
        public PasswordAttribute()
            : base(DataType.Password)
        {
        }

        /// <summary>
        /// Use strong password
        /// </summary>
        public bool StrongPassword { get; set; } = true;

        /// <summary>
        /// Use special characters
        /// </summary>
        public bool SpecialCharacters { get; set; } = true;

        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }
            if (value is string str)
            {
                if (StrongPassword)
                {
                    return SpecialCharacters ? _specialRegex.IsMatch(str) : _strongRegex.IsMatch(str);
                }
                else
                {
                    return _weakRegex.IsMatch(str);
                }
            }
            return false;
        }
    }
}
