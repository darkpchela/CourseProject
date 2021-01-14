using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Services;
using BusinessLayer.Services.BaseCrud;
using BusinessLayer.Services.Validators;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
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

        public static IServiceCollection AddBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICollectionItemCrudService, CollectionItemCrudService>();
            services.AddTransient<ICollectionsCrudService, CollectionsCrudService>();
            services.AddTransient<ICommentsCrudService, CommentsCrudService>();
            services.AddTransient<IItemCommentCrudService, ItemCommentCrudService>();
            services.AddTransient<IItemLikeCrudService, ItemLikeCrudService>();
            services.AddTransient<IItemOptionalFieldCrudService, ItemOptionalFieldCrudService>();
            services.AddTransient<IItemsCrudService, ItemsCrudService>();
            services.AddTransient<IItemTagCrudService, ItemTagCrudService>();
            services.AddTransient<IOptionalFieldsCrudService, OptionalFieldsCrudService>();
            services.AddTransient<ITagsCrudService, TagsCrudService>();
            services.AddTransient<IThemesCrudService, ThemesCrudService>();
            services.AddTransient<IUserCrudService, UsersCrudService>();
            services.AddTransient<IUserRegistService, UserRegistService>();
            services.AddTransient<IFieldTypesCrudService, FieldTypesCrudService>();
            services.AddTransient<IResourceCrudService, ResourceCrudService>();
            services.AddTransient<ICollectionsManager, CollectionsManager>();
            services.AddTransient<IItemsManager, ItemsManager>();
            services.AddTransient<IResourcesManager, ResourcesManager>();
            services.AddTransient<IOptionalFieldsManager, OptionalFieldsManager>();
            services.AddCloudinaryService(configuration);
            services.AddValidators();
            return services;
        }

        public static IServiceCollection AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCloudinary(new Account(configuration["Cloudinary:CloudName"], configuration["Cloudinary:ClientId"], configuration["Cloudinary:ClientSecret"]));
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<ICreateCollectionModelValidator, CreateCollectionModelValidator>();
            services.AddTransient<IUpdateCollectionModelValidator, UpdateCollectionModelValidator>();
            services.AddTransient<ICreateItemModelValidator, CreateItemModelValidator>();
            services.AddTransient<IUpdateItemModelValidator, UpdateItemModelValidator>();
            services.AddTransient<ICreateDefaultOptionalFieldModelValidator, CreateDefaultOptionalFieldModelValidator>();
            services.AddTransient<IDeleteOptionalFieldModelValidator, DeleteOptionalFieldModelValidator>();
            services.AddTransient<IValidationUnitOfWork, ValidationUnitOfWork>();
            return services;
        }
    }
}