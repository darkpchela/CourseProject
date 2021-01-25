using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class Admin : Controller
    {
        private readonly ISessionHelper sessionHelper;

        private readonly IAppUsersManager usersManager;

        private readonly IThemesCrudService themesCrudService;

        private readonly IMapper mapper;

        public Admin(ISessionHelper sessionHelper, IAppUsersManager usersManager, IThemesCrudService themesCrudService, IMapper mapper)
        {
            this.sessionHelper = sessionHelper;
            this.usersManager = usersManager;
            this.themesCrudService = themesCrudService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Main()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Themes()
        {
            var themes = await themesCrudService.GetAllAsync();
            var vms = mapper.Map<IEnumerable<ThemeVM>>(themes);
            return View(vms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTheme(string name)
        {
            var theme = (await themesCrudService.GetAllAsync()).Where(t => t.Name == name);
            if (theme is null)
                return Json(null);
            var model = new ThemeModel
            {
                Name = name
            };
            await themesCrudService.CreateAsync(model);
            var vm = mapper.Map<ThemeVM>(model);
            return Json(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTheme(int id)
        {
            await themesCrudService.DeleteAsync(id);
            return Json("OK");
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
            var usersVM = mapper.Map<IEnumerable<AppUserVM>>(users.Where(u => u.Id != sessionHelper.GetCurrentUserId()));
            return View(usersVM);
        }

        [HttpPost]
        public async Task<IActionResult> Block(int[] users)
        {
            var result = await usersManager.BlockUsersAsync(users);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(int[] users)
        {
            var result = await usersManager.UnblockUsersAsync(users);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int[] users)
        {
            var result = await usersManager.DeleteUsersAsync(users);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminRules(int userId)
        {
            var result = await usersManager.EnableAdminRules(userId);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DropAdminRules(int userId)
        {
            var result = await usersManager.DisableAdminRules(userId);
            return Json(result);
        }
    }
}