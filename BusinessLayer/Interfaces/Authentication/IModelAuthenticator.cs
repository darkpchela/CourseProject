using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Authentication
{
    public interface IModelAuthenticator<TModel> where TModel : IAuthenticatableModel
    {
        Task<ModelAuthenticationResult> AuthenticateAsync(TModel model);
    }
}