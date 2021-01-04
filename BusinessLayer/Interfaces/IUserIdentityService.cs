using BusinessLayer.Models;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserIdentityService : IDisposable
    {
        Task<bool> SignUpAsync(SignUpModel signUpModel);

        Task<bool> SignInAsync(SignInModel userSignInModel);

        Task SignOutAsync();

        Task DeleteAsync(int userId);
    }
}