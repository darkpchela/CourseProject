using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AppSignInManager : IAppSignInManager
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        private readonly IUserCrudService userCrudService;

        private readonly IMapper mapper;

        public AppSignInManager(IIdentityUnitOfWork identityUnitOfWork, IUserCrudService userCrudService, IMapper mapper)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.userCrudService = userCrudService;
            this.mapper = mapper;
        }

        #region Disposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    identityUnitOfWork.Dispose();
                }
                disposed = true;
            }
        }

        ~AppSignInManager()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Disposable

        public async Task<AppRegistResult> RegistAsync(SignUpModel signUpModel)
        {
            var appUser = mapper.Map<AppUser>(signUpModel);
            var result = await identityUnitOfWork.UserManager.CreateAsync(appUser, signUpModel.Password);
            if (!result.Succeeded)
                return new AppRegistResult(result);
            var user = mapper.Map<UserModel>(signUpModel);
            user.Id = appUser.Id;
            await userCrudService.CreateAsync(user);
            return new AppRegistResult();
        }

        public async Task<AppSignInResult> SignInAsync(SignInModel model)
        {
            var result = await identityUnitOfWork.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            var appResult = new AppSignInResult();
            if (!result.Succeeded)
                appResult.AddError("Invalid login/password");
            return new AppSignInResult();
        }


        public async Task<AppRegistResult> ExternalRegistAsync()
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            var appUser = await identityUnitOfWork.UserManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
            if (appUser is null)
            {
                appUser = mapper.Map<AppUser>(info);
                var createRes = await identityUnitOfWork.UserManager.CreateAsync(appUser);
                if (!createRes.Succeeded)
                    return new AppRegistResult(createRes);
            }
            var addLoginRes = await identityUnitOfWork.UserManager.AddLoginAsync(appUser, info);
            if (!addLoginRes.Succeeded)
                return new AppRegistResult(addLoginRes);
            var user = await userCrudService.GetAsync(appUser.Id);
            if (user is null)
            {
                user = mapper.Map<UserModel>(info);
                user.Id = appUser.Id;
                await userCrudService.CreateAsync(user);
            }
            return new AppRegistResult();
        }

        public async Task<AppSignInResult> ExternalSignInAsync()
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            var res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            var appResult = new AppSignInResult();
            if (!res.Succeeded)
                appResult.AddError("External login failed");
            return appResult;
        }

        public async Task SignOutAsync()
        {
            await identityUnitOfWork.SignInManager.SignOutAsync();
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
        }

        public AuthenticationProperties GetExternalAuthenticationProperties(string provider, string url)
        {
            var props = identityUnitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, url);
            return props;
        }
    }
}