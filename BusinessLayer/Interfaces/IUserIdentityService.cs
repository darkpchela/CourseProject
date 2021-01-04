using BusinessLayer.Models;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserIdentityService : IDisposable
    {
        Task<bool> SignUpAsync(UserSignUpModel signUpModel);

        Task<bool> SignInAsync(UserSignInModel userSignInModel);

        Task SignOutAsync();

        Task DeleteAsync(int userId);
    }
}