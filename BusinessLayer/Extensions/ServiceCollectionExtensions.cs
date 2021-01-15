using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Services;
using BusinessLayer.Services.Authentication;
using BusinessLayer.Services.BaseCrud;
using BusinessLayer.Services.Validation;
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

            services.AddBaseCrudServices();
            services.AddTransient<ICollectionsManager, CollectionsManager>();
            services.AddTransient<IItemsManager, ItemsManager>();
            services.AddTransient<IResourcesManager, ResourcesManager>();
            services.AddTransient<IOptionalFieldsManager, OptionalFieldsManager>();
            services.AddCloudinaryService(configuration);
            services.AddAuthenticators();
            services.AddValidators();
            return services;
        }

        private static IServiceCollection AddBaseCrudServices(this IServiceCollection services)
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
            return services;
        }

        private static IServiceCollection AddCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCloudinary(new Account(configuration["Cloudinary:CloudName"], configuration["Cloudinary:ClientId"], configuration["Cloudinary:ClientSecret"]));
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            return services;
        }

        private static IServiceCollection AddAuthenticators(this IServiceCollection services)
        {
            services.AddTransient<ICreateCollectionModelAuthenticator, CreateCollectionModelAuthenticator>();
            services.AddTransient<ICreateItemModelAuthenticator, CreateItemModelAuthenticator>();
            services.AddTransient<ICreateOptionalFieldModelAuthenticator, CreateOptionalFieldModelAuthenticator>();
            services.AddTransient<IDeleteCollectionModelAuthenticator, DeleteCollectionModelAuthenticator>();
            services.AddTransient<IDeleteItemModelAuthenticator, DeleteItemModelAuthenticator>();
            services.AddTransient<IDeleteOptionalFieldModelAuthenticator, DeleteOptionalFieldModelAuthenticator>();
            services.AddTransient<IUpdateCollectionModelAuthenticator, UpdateCollectionModelAuthenticator>();
            services.AddTransient<IUpdateItemModelAuthenticator, UpdateItemModelAuthenticator>();
            services.AddTransient<IModelAuthenticatorsStore, ModelAuthenticatorsStore>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<ICreateCollectionModelValidator, CreateCollectionModelValidator>();
            services.AddTransient<IUpdateCollectionModelValidator, UpdateCollectionModelValidator>();
            services.AddTransient<ICreateItemModelValidator, CreateItemModelValidator>();
            services.AddTransient<IUpdateItemModelValidator, UpdateItemModelValidator>();
            services.AddTransient<IDeleteItemModelValidator, DeleteItemModelValidator>();
            services.AddTransient<ICreateDefaultOptionalFieldModelValidator, CreateDefaultOptionalFieldModelValidator>();
            services.AddTransient<IDeleteOptionalFieldModelValidator, DeleteOptionalFieldModelValidator>();
            services.AddTransient<IDeleteCollectionModelValidator, DeleteCollectionModelValidator>();
            services.AddTransient<IModelValidatorsStore, ModelValidatorsStore>();
            return services;
        }
    }
}