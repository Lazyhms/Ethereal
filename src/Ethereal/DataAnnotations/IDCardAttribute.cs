// Copyright (c) Ethereal. All rights reserved.

using Ethereal.NETCore;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validate IDCard
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class IDCardAttribute : DataTypeAttribute
    {
        private readonly char[] _validate = new[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
        private readonly int[] _weight = new[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        /// <summary>
        /// Initializes a new instance of the <see cref="IDCardAttribute"/> class.
        /// </summary>
        public IDCardAttribute(string? errorMessage = default) : base("IDCard")
            => ErrorMessage = !string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : CoreStrings.IDCardAttribute_Invalid;

        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }
            if (value is string valueAsString && valueAsString.Length == 18)
            {
                var sum = 0;
                for (var i = 0; i < 17; i++)
                {
                    sum += Convert.ToInt32(valueAsString[i]) * _weight[i];
                }
                return _validate[sum % 11] == valueAsString[17];
            }
            return false;
        }
    }
}