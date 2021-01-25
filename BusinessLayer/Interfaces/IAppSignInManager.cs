using BusinessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAppSignInManager : IDisposable
    {
        Task<bool> ExternalRegistAsync(ExternalLoginInfo info);

        Task<bool> RegistAsync(SignUpModel userSignInModel);
    }
}
