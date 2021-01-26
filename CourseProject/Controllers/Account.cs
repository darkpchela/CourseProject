using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Account : Controller
    {
        private readonly IAppSignInManager signInManager;

        public Account(IAppSignInManager signInManager)
        {
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
            if (!res.Succeed)
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error);
                }
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
            var res = await signInManager.SignInAsync(model);
            if (!res.Succeed)
            {
                ModelState.AddModelError("", "Ivalid login/password");
                return View(model);
            }
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }

        [HttpPost]
        public IActionResult ExternalSignUp(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignUp), nameof(Account));
            var props = signInManager.GetExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(props, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalSignUp()
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            var model = new ExternalSignUpVM
            {
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)
            };
            return View(model);
        }

        public async Task<IActionResult> ExternalSignUpConfirm()
        {
            var res = await signInManager.ExternalRegistAsync();
            if (!res.Succeed)
            {
                res.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(nameof(Account.ExternalSignUp));
            }
            return RedirectToAction(nameof(Account.ExternalSignInConfirm), nameof(Account));
        }


        [HttpPost]
        public IActionResult ExternalSignIn(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignIn), nameof(Account));
            var props = signInManager.GetExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(props, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalSignIn()
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                ModelState.AddModelError("", "External login failed");
                return RedirectToAction(nameof(Account.SignIn), nameof(Account));
            }
            var model = new ExternalSignInVM
            {
                Username = info.Principal.FindFirstValue(ClaimTypes.Email)
            };
            return View(model);
        }

        public async Task<IActionResult> ExternalSignInConfirm()
        {
            var res = await signInManager.ExternalSignInAsync();
            if (!res.Succeed)
            {
                res.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return RedirectToAction(nameof(Account.ExternalSignUp), nameof(Account));
            }
            return RedirectToAction(nameof(Profile.Info), nameof(Profile));
        }


        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }
    }
}