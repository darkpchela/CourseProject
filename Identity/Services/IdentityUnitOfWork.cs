using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Services
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        public UserManager<AppUser> UserManager { get; }

        public SignInManager<AppUser> SignInManager { get; }

        public IdentityUnitOfWork(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region Disposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                }
                disposed = true;
            }
        }

        ~IdentityUnitOfWork()
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