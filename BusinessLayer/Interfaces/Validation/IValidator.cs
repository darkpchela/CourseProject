using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Validation
{
    public interface IValidator<TModel> where TModel : class
    {
        Task<ValidationResult> ValidateAsync(TModel model);
    }
}