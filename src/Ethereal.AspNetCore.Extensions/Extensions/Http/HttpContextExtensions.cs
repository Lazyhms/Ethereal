// Copyright (c) Ethereal. All rights reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authentication
{
    /// <summary>
    /// HttpContextExtensions
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// GetAuthenticationProvidersAsync
        /// </summary>
        public static async Task<AuthenticationScheme[]> GetAuthenticationProvidersAsync(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

            return (from scheme in await schemes.GetAllSchemesAsync()
                    where !string.IsNullOrEmpty(scheme.DisplayName)
                    select scheme).ToArray();
        }

        /// <summary>
        /// IsProviderSupportedAsync
        /// </summary>
        public static async Task<bool> IsAuthenticationProviderSupportedAsync(this HttpContext context, string provider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return (from scheme in await context.GetAuthenticationProvidersAsync()
                    where string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase)
                    select scheme).Any();
        }
    }
}
