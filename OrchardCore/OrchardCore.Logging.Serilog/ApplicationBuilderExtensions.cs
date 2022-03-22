using System;
using Microsoft.AspNetCore.Builder;

namespace OrchardCore.Logging
{
    public static class ApplicationBuilderExtensions
    {
        [Obsolete]
        public static IApplicationBuilder UseSerilogTenantNameLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SerilogTenantNameLoggingMiddleware>();
        }

        public static IApplicationBuilder UseSerilogTenantNameLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SerilogTenantNameLoggingMiddleware>();
        }
    }
}
