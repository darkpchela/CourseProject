using BusinessLayer.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLayer.Services.Authentication
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            int.TryParse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            return userId;
        }

        public int GetRemeberedUserId()
        {
            var id = httpContextAccessor.HttpContext.Session.GetInt32("userId");
            if (!id.HasValue)
                return 0;
            return id.Value;
        }

        public void RememberUserId(int id)
        {
            httpContextAccessor.HttpContext.Session.SetInt32("userId", id);
        }
    }
}