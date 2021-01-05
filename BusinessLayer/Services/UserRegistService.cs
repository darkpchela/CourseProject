using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserRegistService : IUserRegistService
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        private readonly ICPUnitOfWork cPUnitOfWork;

        private readonly IMapper mapper;

        public UserRegistService(IIdentityUnitOfWork identityUnitOfWork, ICPUnitOfWork cPUnitOfWork, IMapper mapper)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.cPUnitOfWork = cPUnitOfWork;
            this.mapper = mapper;
        }

        public Task DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExternalRegistAsync(ExternalLoginInfo info)
        {
            var appUser = mapper.Map<AppUser>(info);
            var res = await identityUnitOfWork.UserManager.CreateAsync(appUser);
            if (!res.Succeeded)
                return false;
            await identityUnitOfWork.UserManager.AddLoginAsync(appUser, info);
            var user = mapper.Map<User>(info);
            user.Id = appUser.Id;
            await cPUnitOfWork.UsersRepository.Create(user);
            await cPUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegistAsync(SignUpModel signUpModel)
        {
            var appUser = mapper.Map<AppUser>(signUpModel);
            var res = await identityUnitOfWork.UserManager.CreateAsync(appUser, signUpModel.Password);
            if (!res.Succeeded)
                return false;
            var user = mapper.Map<User>(signUpModel);
            user.Id = appUser.Id;
            await cPUnitOfWork.UsersRepository.Create(user);
            await cPUnitOfWork.SaveChangesAsync();
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
                    cPUnitOfWork.Dispose();
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
