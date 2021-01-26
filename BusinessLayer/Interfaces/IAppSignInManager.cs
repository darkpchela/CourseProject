using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAppSignInManager : IDisposable
    {
        Task<AppRegistResult> RegistAsync(SignUpModel userSignInModel);

        Task<AppSignInResult> SignInAsync(SignInModel model);

        Task<AppRegistResult> ExternalRegistAsync();

        Task<AppSignInResult> ExternalSignInAsync();

        Task SignOutAsync();

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();

        AuthenticationProperties GetExternalAuthenticationProperties(string provider, string url);
    }
}