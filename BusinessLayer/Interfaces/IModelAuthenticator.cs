using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IModelAuthenticator
    {
        Task<IAuthenticatableModel> Authenticate(IAuthenticatableModel authenticatableModel);
    }
}