using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Validators
{
    public interface IValidator<TModel, TResult> where TModel : class where TResult : ResultModel<string>, new()
    {
        Task<TResult> ValidateAsync(TModel model);
    }
}