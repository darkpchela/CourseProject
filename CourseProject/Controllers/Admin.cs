using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using CourseProject.ViewModels.EditableModels;
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

        private readonly IThemesManager themesManager;

        private readonly IResourcesManager resourcesManager;

        private readonly IMapper mapper;

        public Admin(ISessionHelper sessionHelper, IAppUsersManager usersManager, IThemesCrudService themesCrudService, IResourcesManager resourcesManager, IThemesManager themesManager, IMapper mapper)
        {
            this.sessionHelper = sessionHelper;
            this.usersManager = usersManager;
            this.themesCrudService = themesCrudService;
            this.themesManager = themesManager;
            this.resourcesManager = resourcesManager;
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
        public async Task<IActionResult> CreateTheme(CreateThemeVM model)
        {
            var dtoModel = mapper.Map<CreateThemeModel>(model);
            var result = await themesManager.CreateAsync(dtoModel);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTheme(DeleteThemeVM model)
        {
            var dtoModel = mapper.Map<DeleteThemeModel>(model);
            var result = await themesManager.DeleteAsync(dtoModel);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTheme(UpdateThemeVM model)
        {
            var dtoModel = mapper.Map<UpdateThemeModel>(model);
            var result = await themesManager.UpdateAsync(dtoModel);
            return Json(result);
        }

        public IActionResult Resources()
        {
            return View();
        }

        public async Task<IActionResult> RemoveGarbage()
        {
            await resourcesManager.RemoveGarbageAsync();
            return RedirectToAction(nameof(Admin.Resources), nameof(Admin));
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
        public async Task<IActionResult> DeleteUsers(int[] users)
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