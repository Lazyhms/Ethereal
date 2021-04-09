// Copyright (c) Ethereal. All rights reserved.

using Ethereal.AspNetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ethereal.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies the maximum length of IFormFile or IFormFileCollection data allowed in a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FileLengthAttribute : DataTypeAttribute
    {
        private readonly long _length;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLengthAttribute"/> class.
        /// </summary>
        public FileLengthAttribute(long length) : base("FileLength")
        {
            ErrorMessage = CoreStrings.FileLengthAttribute_Invalid;
            _length = length;
        }

        /// <summary>
        /// Maximum KB Length
        /// </summary>
        public long Length => _length * 1024 * 1024;

        /// <summary>
        /// IsValid
        /// </summary>
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (Length <= 0L)
            {
                throw new InvalidOperationException(CoreStrings.FileLengthAttribute_InvalidOperation);
            }

            long length;
            if (value is IFormFile formFile)
            {
                length = formFile.Length;
            }
            else if (value is IFormFileCollection formFiles)
            {
                length = formFiles.Count;
            }
            else
            {
                throw new InvalidCastException(CoreStrings.FileLengthAttribute_InvalidCast);
            }

            return length <= Length;
        }
    }
}