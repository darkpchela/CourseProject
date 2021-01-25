using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class Admin : Controller
    {
        private readonly ISessionHelper sessionHelper;

        private readonly IAppUsersManager usersManager;

        private readonly IMapper mapper;

        public Admin(ISessionHelper sessionHelper, IAppUsersManager usersManager, IMapper mapper)
        {
            this.sessionHelper = sessionHelper;
            this.usersManager = usersManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult ContinueAs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContinueAs(bool asMe)
        {
            int userId = asMe ? sessionHelper.GetCurrentUserId() : sessionHelper.GetRemeberedUserId();
            sessionHelper.RememberUserId(userId);
            return RedirectToAction(nameof(Profile.Info), nameof(Profile), new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await usersManager.GetAllUsersAsync();
            var usersVM = mapper.Map<IEnumerable<AppUserVM>>(users);
            return View(usersVM);
        }

        [HttpPost]
        public async Task<IActionResult> Block(int[] users)
        {
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(int[] users)
        {
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int[] users)
        {
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminRules(int userId)
        {
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> DropAdminRules(int userId)
        {
            return Json(null);
        }
    }
}