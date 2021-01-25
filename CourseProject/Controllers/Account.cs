using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using CourseProject.ViewModels;
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

        private readonly IAppSignInManager signInManager;

        public Account(IIdentityUnitOfWork identityUnitOfWork, IAppSignInManager signInManager)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.signInManager = signInManager;
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
            var res = await signInManager.RegistAsync(model);
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
            var res = await identityUnitOfWork.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if(!res.Succeeded)
            {
                ModelState.AddModelError("","Incorrect login/password");
                return View(model);
            }
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }

        [HttpGet]
        public async Task<IActionResult> ExternalSignUp()
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            var model = new ExternalSignUpVM
            {
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExternalSignUp(ExternalSignUpVM model)
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                ModelState.AddModelError("", "Failed to fetch external data");
                return View(model);
            }
            var res = await signInManager.ExternalRegistAsync(info);
            if (!res)
            {
                ModelState.AddModelError("", "Failed to sign up");
                return View(model);
            }
            return RedirectToAction(nameof(Home.Index), nameof(Home));
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
            var model = new ExternalSignInVM
            {
                Username = info.Principal.FindFirstValue(ClaimTypes.Email)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ExternalSignIn(ExternalSignInVM model)
        {
            var info = await identityUnitOfWork.SignInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                ModelState.AddModelError("", "Failed to fetch external data");
                return View(model);
            }
            var res = await identityUnitOfWork.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (!res.Succeeded)
                return RedirectToAction(nameof(ExternalSignUp), nameof(Account));
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }

        [HttpPost]
        public IActionResult ExternalSignInRequest(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignIn), nameof(Account));
            var props = identityUnitOfWork.SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(props, provider);
        }

        public async Task<IActionResult> SignOut()
        {
            await identityUnitOfWork.SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }
    }
}