using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models.ResultModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public abstract class DefaultValidator<TModel> : IValidator<TModel> where TModel : class
    {
        protected ValidationResult result;

        protected readonly IHttpContextAccessor httpContextAccessor;

        public DefaultValidator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ValidationResult> ValidateAsync(TModel model)
        {
            result = new ValidationResult();
            await BaseValidation(model);
            if (!result.Succeed)
                return result;
            await OptionalValidation(model);
            return result;
        }

        protected abstract Task BaseValidation(TModel model);

        protected abstract Task OptionalValidation(TModel model);
    }
}