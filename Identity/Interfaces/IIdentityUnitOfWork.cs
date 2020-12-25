using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }

        SignInManager<User> SignInManager { get; }
    }
}