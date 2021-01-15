using BusinessLayer.Interfaces.Authenticators;
using BusinessLayer.Models.ResultModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authenticators
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
            return authenticationResult;
        }

        protected abstract Task<bool> TrySetOwner(TModel model);

        private void AuthenticateRequest(TModel model)
        {
            var isAdminRequest = httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int requesterId);
            if (!isAdminRequest && model.OwnerId != requesterId)
                authenticationResult.AddError("Authnetication error");
        }
    }
}