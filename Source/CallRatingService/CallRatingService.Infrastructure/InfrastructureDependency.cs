using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallRatingService.Infrastructure
{
    public static class InfrastructureDependency
    {
        public static IServiceCollection AddCallRatingDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CallRatingServiceDbContext>(options => options.UseSqlite(connectionString));

            services.AddScoped<ICallRatingServiceDbContext, CallRatingServiceDbContext>();

            return services;
        }
    }
}
