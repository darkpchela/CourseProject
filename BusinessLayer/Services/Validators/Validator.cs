using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models.ResultModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public abstract class Validator<TModel, TResultModel> : IValidator<TModel, TResultModel> where TModel : class where TResultModel : ResultModel<string>, new()
    {
        protected TResultModel result;

        protected readonly IHttpContextAccessor httpContextAccessor;

        public Validator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResultModel> ValidateAsync(TModel model)
        {
            result = new TResultModel();
            await BaseValidation(model);
            if (!result.Succeed)
                return result;
            await OptionalValidation(model);
            return result;
        }

        protected abstract Task BaseValidation(TModel model);

        protected abstract Task OptionalValidation(TModel model);

        protected virtual void Authenticate(IAuthenticatableModel model)
        {
            var isAdminRequest = httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int requesterId);
            if (!isAdminRequest && model.OwnerId != requesterId)
                result.AddError("Access denied");
        }
    }
}