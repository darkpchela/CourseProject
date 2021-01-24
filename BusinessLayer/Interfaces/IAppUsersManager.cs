using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
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

        Task<DeleteUsersResult> DeleteUsersAsync(IEnumerable<int> userIds);

        Task<BlockUsersResult> BlockUsersAsync(IEnumerable<int> userIds);

        Task<UnblockUsersResult> UnblockUsersAsync(IEnumerable<int> userIds);

        Task<EnableAdminRulesResult> EnableAdminRules(int userId);

        Task<DisableAdminRulesResult> DisableAdminRules(int userId);
    }
}