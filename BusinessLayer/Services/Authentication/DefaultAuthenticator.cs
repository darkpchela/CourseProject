using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.ResultModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public abstract class DefaultAuthenticator<TModel> : IModelAuthenticator<TModel> where TModel : IAuthenticatableModel
    {
        protected ModelAuthenticationResult authenticationResult;

        private readonly IHttpContextAccessor httpContextAccessor;

        public DefaultAuthenticator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ModelAuthenticationResult> AuthenticateAsync(TModel model)
        {
            authenticationResult = new ModelAuthenticationResult();
            if (!await TrySetOwner(model))
            {
                authenticationResult.AddError("Owner not found");
                return authenticationResult;
            }
            AuthenticateRequest(model);
            return authenticationResult;
        }

        protected abstract Task<bool> TrySetOwner(TModel model);

        private void AuthenticateRequest(TModel model)
        {
            var isAdminRequest = httpContextAccessor.HttpContext.User.IsInRole("admin");
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int requesterId);
            if (!isAdminRequest && model.OwnerId != requesterId)
                authenticationResult.AddError("Authnetication error");
        }
    }
}