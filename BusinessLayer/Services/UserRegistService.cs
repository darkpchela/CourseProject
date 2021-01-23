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
    public class UserRegistService : IUserRegistService
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        private readonly IMapper mapper;

        private readonly IUserCrudService userCrudService;

        public UserRegistService(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper, IUserCrudService userCrudService)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.mapper = mapper;
            this.userCrudService = userCrudService;
        }

        public Task DeleteAsync(int userId)
        {
            throw new NotImplementedException();
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

        ~UserRegistService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Disposable
    }
}