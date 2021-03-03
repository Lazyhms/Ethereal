// Copyright (c) Ethereal. All rights reserved.
//

using Ethereal.ComponentModel.DataAnnotations;
using Ethereal.Extensions;
using Ethereal.Utilities;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        public static IServiceCollection RegisterAssemblyTypes(
            [NotNull] this IServiceCollection services,
            [NotNull] string assemblyName)
        {
            Check.NotEmpty(assemblyName, nameof(assemblyName));

            return services.RegisterAssemblyTypes(Assembly.Load(assemblyName));
        }

        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        public static IServiceCollection RegisterAssemblyTypes(
            [NotNull] this IServiceCollection services,
            [NotNull] string assemblyName,
            [NotNull] string searchPattern)
        {
            Check.NotEmpty(assemblyName, nameof(assemblyName));

            throw new NotImplementedException("not implemented");
        }

        /// <summary>
        /// RegisterAssemblyTypes
        /// </summary>
        public static IServiceCollection RegisterAssemblyTypes(
            [NotNull] this IServiceCollection services,
            [NotNull] Assembly assembly)
        {
            Check.NotNull(assembly, nameof(assembly));

            var serviceTypes = assembly.GetTypes();
            foreach (var serviceType in serviceTypes.Where(st => st.IsInterface))
            {
                var serviceDescriptor = serviceType.GetCustomAttribute<ServiceDescriptorAttribute>();
                if (serviceDescriptor is null)
                {
                    continue;
                }
                var implementationTypes = serviceTypes.Where(implementationType => serviceType.IsAssignableFrom(implementationType) && serviceType != implementationType);
                if (implementationTypes is null || implementationTypes.Count() == 0)
                {
                    throw new NotImplementedException(string.Format(CoreStrings.NotImplemented_Exception, serviceType.Name));
                }
                foreach (var implementationType in implementationTypes)
                {
                    switch (serviceDescriptor!.ServiceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(serviceType, implementationType!);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(serviceType, implementationType!);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(serviceType, implementationType!);
                            break;
                        default:
                            break;
                    }
                }
            }

            return services;
        }
    }
}
