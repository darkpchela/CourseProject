using BusinessLayer.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    public class Admin : Controller
    {
        private readonly ISessionHelper sessionHelper;

        public Admin(ISessionHelper sessionHelper)
        {
            this.sessionHelper = sessionHelper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ContinueAs()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult ContinueAs(bool asMe)
        {
            int userId = asMe ? sessionHelper.GetCurrentUserId() : sessionHelper.GetRemeberedUserId();
            sessionHelper.RememberUserId(userId);
            return RedirectToAction(nameof(Profile.Info), nameof(Profile), new { id = userId });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}