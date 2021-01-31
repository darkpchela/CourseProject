using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using DataAccessLayer.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CPDbContext>(o =>
            {
                o.UseLazyLoadingProxies();
                o.UseSqlServer(configuration.GetConnectionString("ApplicationDb"));
            });
            return services;
        }

        public static IServiceCollection AddApplicationDbDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICollectionItemRepository, CollectionItemRepository>();
            services.AddTransient<ICollectionsRepository, CollectionsRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
            services.AddTransient<IItemCommentRepository, ItemCommentRepository>();
            services.AddTransient<IItemLikeRepository, ItemLikeRepository>();
            services.AddTransient<IItemOptionalFieldRepository, ItemOptionalFieldRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IItemTagRepository, ItemTagRepository>();
            services.AddTransient<IOptionalFieldsRepository, OptionalFieldsRepository>();
            services.AddTransient<ITagsRepository, TagsRepository>();
            services.AddTransient<IThemesRepository, ThemesRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IFieldTypesRepository, FieldTypesRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddScoped<ICPUnitOfWork, CPUnitOfWork>();
            return services;
        }
    }
}