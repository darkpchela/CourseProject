using CourseProject.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CourseProject.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseStatusValidation(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<UserStatusValidator>();
            return applicationBuilder;
        }
    }
}