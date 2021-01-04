using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.Interfaces;
using Identity.Entities;
using Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        private readonly ICPUnitOfWork cPUnitOfWork;

        public UserIdentityService(IIdentityUnitOfWork identityUnitOfWork, ICPUnitOfWork cPUnitOfWork)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.cPUnitOfWork = cPUnitOfWork;
        }

        public Task DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SignInAsync(SignInModel userSignInModel)
        {
            var res = await identityUnitOfWork.SignInManager.PasswordSignInAsync(userSignInModel.Email, userSignInModel.Password, userSignInModel.RememberMe, false);
            return res.Succeeded;
        }

        public async Task SignOutAsync()
        {
           await identityUnitOfWork.SignInManager.SignOutAsync();
        }

        public async Task<bool> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new User();
            var res = await identityUnitOfWork.UserManager.CreateAsync(user, signUpModel.Password);
            return res.Succeeded;
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

        ~UserIdentityService()
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
