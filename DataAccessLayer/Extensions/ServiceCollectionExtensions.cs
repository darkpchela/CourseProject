using DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CPDbContext>(o =>
            {
                o.UseLazyLoadingProxies();
                o.UseSqlServer(configuration.GetConnectionString("ApplicationDb"));
            });
            return services;
        }
    }
}