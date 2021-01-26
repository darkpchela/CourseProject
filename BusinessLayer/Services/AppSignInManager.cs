using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using Identity.Entities;
using Identity.Interfaces;
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

        public async Task<bool> RegistAsync(SignUpModel signUpModel)
        {
            var appUser = mapper.Map<AppUser>(signUpModel);
            var res = await identityUnitOfWork.UserManager.CreateAsync(appUser, signUpModel.Password);
            if (!res.Succeeded)
                return false;
            var user = mapper.Map<UserModel>(signUpModel);
            user.Id = appUser.Id;
            await userCrudService.CreateAsync(user);
            return true;
        }

        public async Task<bool> ExternalRegistAsync(ExternalLoginInfo info)
        {
            var appUser = await identityUnitOfWork.UserManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
            if (appUser is null)
            {
                appUser = mapper.Map<AppUser>(info);
                var res = await identityUnitOfWork.UserManager.CreateAsync(appUser);
                if (!res.Succeeded)
                    return false;
            }
            await identityUnitOfWork.UserManager.AddLoginAsync(appUser, info);
            var user = await userCrudService.GetAsync(appUser.Id);
            if (user is null)
            {
                user = mapper.Map<UserModel>(info);
                user.Id = appUser.Id;
                await userCrudService.CreateAsync(user);
            }
            return true;
        }

        public async Task SignOutAsync()
        {
            await identityUnitOfWork.SignInManager.SignOutAsync();
        }
    }
}