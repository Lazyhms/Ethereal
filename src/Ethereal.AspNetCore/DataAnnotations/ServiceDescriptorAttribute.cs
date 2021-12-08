// Copyright (c) Ethereal. All rights reserved.

using Microsoft.Extensions.DependencyInjection;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// ServiceDescriptorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class LifetimeDescriptorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LifetimeDescriptorAttribute"/> class.
        /// </summary>
        public LifetimeDescriptorAttribute(ServiceLifetime serviceLifetime)
            => ServiceLifetime = serviceLifetime;

        /// <summary>
        /// ServiceLifetime
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }
    }
}