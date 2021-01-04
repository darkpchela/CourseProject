using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Account : Controller
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;

        private readonly IUserIdentityService userIdentityService;

        public Account(IIdentityUnitOfWork identityUnitOfWork)
        {
            this.identityUnitOfWork = identityUnitOfWork;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new SignUpModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var res = await userIdentityService.SignUpAsync(model);
            if (!res)
            {
                ModelState.AddModelError("", "Username or email already taken");
                return View(model);
            }
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExternalSignUp()
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            var model = new ExternalSignUpModel
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExternalSignUp(ExternalSignUpModel model)
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            var user = new User
            {
                UserName = model.Username,
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value
            };
            await identityUnitOfWork.UserManager.CreateAsync(user);
            await identityUnitOfWork.UserManager.AddLoginAsync(user, info);
            return RedirectToAction(nameof(ExternalSignIn));
        }

        [HttpPost]
        public IActionResult ExternalSignUpRequest(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignUp), nameof(Account));
            var props = identityUnitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(props, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalSignIn()
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            if (info is null)
                return RedirectToAction(nameof(SignIn));
            var res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (!res.Succeeded)
                return RedirectToAction(nameof(ExternalSignUp));
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }

        [HttpPost]
        public async Task<IActionResult> ExternalSignIn(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignIn), nameof(Account));
            var props = identityUnitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(props, provider);
        }

        //[HttpGet]
        //public async Task<IActionResult> ExternalLogin()
        //{
        //    var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
        //    if (info is null)
        //        return RedirectToAction(nameof(SignIn));
        //    var res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        //    if (!res.Succeeded)
        //    {
        //        var email = info.Principal.FindFirst(ClaimTypes.Email).Value;
        //        var lastname = info.Principal.FindFirstValue(ClaimTypes.Surname);
        //        var firstname = info.Principal.FindFirstValue(ClaimTypes.GivenName);
        //        if (email != null)
        //        {
        //            var user = await identityUnitOfWork.UserManager.FindByEmailAsync(email);

        //            if (user is null)
        //            {
        //                user = new User
        //                {
        //                    UserName = email,
        //                    Email = email
        //                };
        //                await identityUnitOfWork.UserManager.CreateAsync(user);
        //            }
        //            await identityUnitOfWork.UserManager.AddLoginAsync(user, info);
        //        }
        //        res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        //    }
        //    return RedirectToAction(nameof(SignIn));
        //}

        public async Task<IActionResult> SignOut()
        {
            await userIdentityService.SignOutAsync();
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }
    }
}