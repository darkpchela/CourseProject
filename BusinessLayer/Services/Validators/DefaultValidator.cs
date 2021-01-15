using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public abstract class DefaultValidator<TModel> : IValidator<TModel> where TModel : class
    {
        protected ValidationResult result;

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