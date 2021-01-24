using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AppUsersManager : IAppUsersManager
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        private readonly IMapper mapper;

        private readonly IUserCrudService userCrudService;

        public AppUsersManager(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper, IUserCrudService userCrudService)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.mapper = mapper;
            this.userCrudService = userCrudService;
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

        public async Task<DeleteUsersResult> DeleteUsersAsync(IEnumerable<int> userIds)
        {
            var result = new DeleteUsersResult();
            foreach (var id in userIds)
            {
                var appUser = await identityUnitOfWork.UserManager.FindByIdAsync(id.ToString());
                var idenRes = await identityUnitOfWork.UserManager.DeleteAsync(appUser);
                if (idenRes.Succeeded)
                {
                    await userCrudService.DeleteAsync(id);
                    result.DeletedUsers.Add(id);
                }
                else
                {
                    result.AddError($"User {id} not found");
                    result.NotDeleted.Add(id);
                }
            }
            return result;
        }

        public async Task<BlockUsersResult> BlockUsersAsync(IEnumerable<int> userIds)
        {
            var result = new BlockUsersResult();
            foreach (var id in userIds)
            {
                var appUser = await identityUnitOfWork.UserManager.FindByIdAsync(id.ToString());
                if (appUser is null)
                {
                    result.AddError($"User {id} not found");
                    result.NotBlocked.Add(id);
                    continue;
                }
                var idenRes = await identityUnitOfWork.UserManager.AddToRoleAsync(appUser, "blocked");
                if (!idenRes.Succeeded)
                {
                    result.AddError("Identity error");
                    result.NotBlocked.Add(id);
                    continue;
                }
                result.Blocked.Add(id);
            }
            return result;
        }

        public async Task<UnblockUsersResult> UnblockUsersAsync(IEnumerable<int> userIds)
        {
            var result = new UnblockUsersResult();
            foreach (var id in userIds)
            {
                var appUser = await identityUnitOfWork.UserManager.FindByIdAsync(id.ToString());
                if (appUser is null)
                {
                    result.AddError($"User {id} not found");
                    result.NotUnblocked.Add(id);
                    continue;
                }
                var idenRes = await identityUnitOfWork.UserManager.AddToRoleAsync(appUser, "blocked");
                if (!idenRes.Succeeded)
                {
                    result.AddError("Identity error");
                    result.NotUnblocked.Add(id);
                    continue;
                }
                result.Unblocked.Add(id);
            }
            return result;
        }

        public async Task<EnableAdminRulesResult> EnableAdminRules(int userId)
        {
            var result = new EnableAdminRulesResult();
            var appUser = await identityUnitOfWork.UserManager.FindByIdAsync(userId.ToString());
            if (appUser is null)
            {
                result.AddError("User not found");
                return result;
            }
            var idenRes = await identityUnitOfWork.UserManager.AddToRoleAsync(appUser, "admin");
            if (!idenRes.Succeeded)
            {
                result.AddError("Identity error");
                return result;
            }
            result.UserId = userId;
            return result;
        }

        public async Task<DisableAdminRulesResult> DisableAdminRules(int userId)
        {
            var result = new DisableAdminRulesResult();
            var appUser = await identityUnitOfWork.UserManager.FindByIdAsync(userId.ToString());
            if (appUser is null)
            {
                result.AddError("User not found");
                return result;
            }
            var idenRes = await identityUnitOfWork.UserManager.RemoveFromRoleAsync(appUser, "admin");
            if (!idenRes.Succeeded)
            {
                result.AddError("Identity error");
                return result;
            }
            result.UserId = userId;
            return result;
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

        ~AppUsersManager()
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