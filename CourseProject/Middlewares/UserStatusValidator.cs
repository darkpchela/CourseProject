using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CourseProject.Middlewares
{
    public class UserStatusValidator
    {
        private readonly RequestDelegate _next;

        public UserStatusValidator(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAppSignInManager signInManager)
        {
            if (context.User.IsInRole("blocked"))
            {
                await signInManager.SignOutAsync();
                context.Response.Redirect("/Home/Blocked");
            }
            await _next.Invoke(context);
        }
    }
}