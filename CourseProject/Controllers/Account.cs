using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Account : Controller
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        public Account(IIdentityUnitOfWork identityUnitOfWork)
        {
            this.identityUnitOfWork = identityUnitOfWork;
        }

        public async Task<IActionResult> SignIn()
        {
            

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignOut()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogin()
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            if (info is null)
                return RedirectToAction(nameof(SignIn));
            var res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (!res.Succeeded)
            {
                var email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                if (email != null)
                { 
                    var user = await identityUnitOfWork.UserManager.FindByEmailAsync(email);

                    if (user is null)
                    {
                        user = new User
                        {
                            UserName = email,
                            Email = email
                        };
                        await identityUnitOfWork.UserManager.CreateAsync(user);
                    }
                    await identityUnitOfWork.UserManager.AddLoginAsync(user, info);
                }
                res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            }
            return RedirectToAction(nameof(SignIn));
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLogin), nameof(Account));
            var props = identityUnitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(props, provider);
        }

    }
}
