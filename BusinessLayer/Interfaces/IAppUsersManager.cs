using BusinessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAppUsersManager : IDisposable
    {
        Task<bool> ExternalRegistAsync(ExternalLoginInfo info);

        Task<bool> RegistAsync(SignUpModel userSignInModel);

        Task DeleteUsersAsync(IEnumerable<int> userIds);

        Task BlockUsersAsync(IEnumerable<int> userIds);

        Task UnblockUsersAsync(IEnumerable<int> userIds);

        Task EnableAdminRules(int userId);

        Task DisableAdminRules(int userId);
    }
}