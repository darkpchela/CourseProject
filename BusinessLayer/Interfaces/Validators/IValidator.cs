using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Validators
{
    public interface IValidator<TModel> where TModel : class
    {
        Task<ValidationResult> ValidateAsync(TModel model);
    }
}