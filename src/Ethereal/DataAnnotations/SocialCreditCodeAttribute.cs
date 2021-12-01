// Copyright (c) Ethereal. All rights reserved.

using Ethereal.NETCore;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validate SocialCreditCode
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class SocialCreditCodeAttribute : DataTypeAttribute
    {
        private readonly char[] _codes = new[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'W', 'X', 'Y'
        };

        private readonly int[] _factor = new[] { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialCreditCodeAttribute"/> class.
        /// </summary>
        public SocialCreditCodeAttribute(string? errorMessage = default) : base("SocialCreditCode")
            => ErrorMessage = !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : CoreStrings.SocialCreditCodeAttribute_Invalid;

        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }
            if (value is string valueAsString && valueAsString != null && valueAsString.Length == 18)
            {
                var total = _factor.Select((p, i) => p * Array.IndexOf(_codes, valueAsString[i])).Sum();
                var index = total % 31 == 0 ? 0 : (31 - total % 31);
                return _codes[index] == valueAsString[^1];
            }
            return false;
        }
    }
}