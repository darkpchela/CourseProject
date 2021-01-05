using BusinessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserRegistService : IDisposable
    {
        Task<bool> ExternalRegistAsync(ExternalLoginInfo info);

        Task<bool> RegistAsync(SignUpModel userSignInModel);

        Task DeleteAsync(int userId);
    }
}