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
            services.AddCloudinaryService(configuration);
            services.AddTransient<ICollectionsManager, CollectionsManager>();
            services.AddTransient<IItemsManager, ItemsManager>();
            services.AddTransient<IResourcesManager, ResourcesManager>();
            services.AddTransient<IOptionalFieldsManager, OptionalFieldsManager>();
            services.AddTransient<ITagsManager, TagsManager>();
            services.AddTransient<ILikesManager, LikesManager>();
            services.AddTransient<ICommentsManager, CommentsManager>();
            services.AddTransient<IAppUsersManager, AppUsersManager>();
            services.AddTransient<IAppSignInManager, AppSignInManager>();
            services.AddTransient<IThemesManager, ThemesManager>();
            services.AddAuthenticators();
            services.AddValidators();
            return services;
        }

        private static IServiceCollection AddBaseCrudServices(this IServiceCollection services)
        {
            services.AddScoped<ICollectionItemCrudService, CollectionItemCrudService>();
            services.AddScoped<ICollectionsCrudService, CollectionsCrudService>();
            services.AddScoped<ICommentsCrudService, CommentsCrudService>();
            services.AddScoped<IItemCommentCrudService, ItemCommentCrudService>();
            services.AddScoped<IItemLikeCrudService, ItemLikeCrudService>();
            services.AddScoped<IItemOptionalFieldCrudService, ItemOptionalFieldCrudService>();
            services.AddScoped<IItemsCrudService, ItemsCrudService>();
            services.AddScoped<IItemTagCrudService, ItemTagCrudService>();
            services.AddScoped<IOptionalFieldsCrudService, OptionalFieldsCrudService>();
            services.AddScoped<ITagsCrudService, TagsCrudService>();
            services.AddScoped<IThemesCrudService, ThemesCrudService>();
            services.AddScoped<IUserCrudService, UsersCrudService>();
            services.AddScoped<IFieldTypesCrudService, FieldTypesCrudService>();
            services.AddScoped<IResourceCrudService, ResourceCrudService>();
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
            services.AddSingleton<ISessionHelper, SessionHelper>();
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
            services.AddTransient<IDeleteCollectionModelValidator, DeleteCollectionModelValidator>();
            services.AddTransient<ICreateItemModelValidator, CreateItemModelValidator>();
            services.AddTransient<IUpdateItemModelValidator, UpdateItemModelValidator>();
            services.AddTransient<IDeleteItemModelValidator, DeleteItemModelValidator>();
            services.AddTransient<ICreateDefaultOptionalFieldModelValidator, CreateDefaultOptionalFieldModelValidator>();
            services.AddTransient<IDeleteOptionalFieldModelValidator, DeleteOptionalFieldModelValidator>();
            services.AddTransient<ILikeItemModelValidator, LikeItemModelValidator>();
            services.AddTransient<ICommentItemModelValidator, CommentItemModelValidator>();
            services.AddTransient<IModelValidatorsStore, ModelValidatorsStore>();
            services.AddTransient<ICreateThemeModelValidator, CreateThemeModelValidator>();
            services.AddTransient<IUpdateThemeModelValidator, UpdateThemeModelValidator>();
            services.AddTransient<IDeleteThemeModelValidator, DeleteThemeModelValidator>();
            return services;
        }
    }
}