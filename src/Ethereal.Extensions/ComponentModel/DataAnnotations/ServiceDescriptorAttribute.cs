// Copyright (c) Ethereal. All rights reserved.
//

using Microsoft.Extensions.DependencyInjection;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// ServiceDescriptorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ServiceDescriptorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDescriptorAttribute"/> class.
        /// </summary>
        public ServiceDescriptorAttribute(ServiceLifetime serviceLifetime) => ServiceLifetime = serviceLifetime;

        /// <summary>
        /// ServiceDescriptor
        /// </summary>
        public object? ServiceDescriptor { get; set; } = null;

        /// <summary>
        /// ServiceLifetime
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }
    }
}
