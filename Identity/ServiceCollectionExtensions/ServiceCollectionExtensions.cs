using Identity.Entities;
using Identity.Interfaces;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("Identity");
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddIdentity<User, Role>(configs =>
            {
                configs.User.RequireUniqueEmail = true;
                configs.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 5
                };
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddIdentityDependencies(this IServiceCollection services)
        {
            services.AddTransient<IIdentityUnitOfWork, IdentityUnitOfWork>();
            return services;
        }
    }
}