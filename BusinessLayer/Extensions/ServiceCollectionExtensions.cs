﻿using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace BusinessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services, params Assembly[] otherMapperAssemblies)
        {
            List<Assembly> assemblies = new List<Assembly>(otherMapperAssemblies)
            {
                Assembly.GetExecutingAssembly()
            };
            services.AddAutoMapper(assemblies);
            return services;
        }

        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRegistService, UserRegistService>();
            return services;
        }
    }
}