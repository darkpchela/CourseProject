using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ModelAuthenticator : IModelAuthenticator
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ModelAuthenticator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<IAuthenticatableModel> Authenticate(IAuthenticatableModel authenticatableModel)
        {
            authenticatableModel.IsAdminRequest = httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int parsedId);
            authenticatableModel.RequesterId = parsedId;
            return Task.FromResult(authenticatableModel);
        }
    }
}
