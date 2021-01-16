using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public abstract class DefaultValidator<TModel> : IValidator<TModel> where TModel : class
    {
        protected ValidationResult ValidationResult { get; } = new ValidationResult();

        public async Task<ValidationResult> ValidateAsync(TModel model)
        {

            await BaseValidation(model);
            if (!ValidationResult.Succeed)
                return ValidationResult;
            await OptionalValidation(model);
            return ValidationResult;
        }

        protected abstract Task BaseValidation(TModel model);

        protected abstract Task OptionalValidation(TModel model);
    }
}