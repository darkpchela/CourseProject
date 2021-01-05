using Identity.Entities;
using Identity.Interfaces;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            services.AddIdentity<AppUser, Role>(configs =>
            {
                configs.User.RequireUniqueEmail = true;
                configs.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 3
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

        public static IServiceCollection AddExternalProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn");
                })
                .AddVkontakte("vk", options =>
                {
                    var vkAuth = configuration.GetSection("Authentication:VK");
                    options.ClientId = vkAuth["ClientId"];
                    options.ClientSecret = vkAuth["ClientSecret"];
                    options.Scope.Add("email");
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddGoogle("google", options =>
                {
                    var googleAuth = configuration.GetSection("Authentication:Google");
                    options.ClientId = googleAuth["ClientId"];
                    options.ClientSecret = googleAuth["ClientSecret"];
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                });
            return services;
        }
    }
}