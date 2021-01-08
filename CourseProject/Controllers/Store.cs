using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using CloudinaryDotNet.Actions;
using CourseProject.ViewModels;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Store : Controller
    {
        private readonly ICloudinaryService cloudinaryService;

        private readonly ICPUnitOfWork cPUnitOfWork;

        private readonly IFieldTypesCrudService fieldTypesCrudService;

        private readonly IThemesCrudService themesCrudService;

        public Store(ICloudinaryService cloudinaryService, ICPUnitOfWork cPUnitOfWork,
            IFieldTypesCrudService fieldTypesCrudService, IThemesCrudService themesCrudService)
        {
            this.cloudinaryService = cloudinaryService;
            this.cPUnitOfWork = cPUnitOfWork;
            this.fieldTypesCrudService = fieldTypesCrudService;
            this.themesCrudService = themesCrudService;
        }

        public IActionResult Profile(int? id)
        {
            if (!id.HasValue)
                id = cPUnitOfWork.UsersRepository.GetAll().FirstOrDefault(u => u.Id.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
            var model = new ProfileVM
            {
                Username = User.Identity.Name,
                User = cPUnitOfWork.UsersRepository.GetAll().FirstOrDefault(u => u.Id == id)
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollection()
        {
            var model = new CreateCollectionVM
            {
                FieldTypes = await fieldTypesCrudService.GetAllAsync(),
                Themes = await themesCrudService.GetAllAsync()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCollection(CreateCollectionVM model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View(new CreateItemVM());
        }

        [HttpPost]
        public IActionResult CreateItem(CreateItemVM model)
        {
            return null;
        }

        public IActionResult Collection(int id)
        {
            return View();
        }

        public IActionResult Item(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var fileDesc = new FileDescription(file.FileName, file.OpenReadStream());
            var res = await cloudinaryService.UploadAsync(fileDesc);
            HttpContext.Session.SetString("fileId", res.ObjectId);
            return Json(res);
        }

        [HttpPost]
        public async Task AbortUpload()
        {
            var fileId = HttpContext.Session.GetString("fileId");
            await cloudinaryService.DeleteAsync(fileId);
        }

        public IActionResult _Collections(int id)
        {
            var model = new ProfileCollectionsVM
            {
                Collections = new List<CollectionVM>(),
                IsEditable = true
            };
            return PartialView("Profile/_Collections", model);
        }

        public IActionResult _Items()
        {
            var model = new ProfileItemsVM
            {
                Items = new List<ItemVM>(),
                IsEditable = true
            };
            return PartialView("Profile/_Items", model);
        }
    }
}