using AutoMapper;
using BusinessLayer.Extensions;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using CloudinaryDotNet.Actions;
using CourseProject.ViewModels;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly ICollectionsManager collectionsManager;

        private readonly IItemsManager itemsManager;

        private readonly IMapper mapper;

        private CreateCollectionVM createCollectionTestVM = new CreateCollectionVM()
        {
            Name ="Test",
            Description = "It's a test collection"
        };

        public Store(ICloudinaryService cloudinaryService, ICPUnitOfWork cPUnitOfWork, ICollectionsCrudService collectionsCrudService, ICollectionsManager collectionsManager,
            IItemsManager itemsManager, IMapper mapper)
        {
            this.cloudinaryService = cloudinaryService;
            this.cPUnitOfWork = cPUnitOfWork;
            this.collectionsManager = collectionsManager;
            this.collectionsCrudService = collectionsCrudService;
            this.mapper = mapper;
            this.itemsManager = itemsManager;
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
            var model = createCollectionTestVM;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection(CreateCollectionVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var imageId = HttpContext.Session.GetString("fileId");
            if (string.IsNullOrEmpty(imageId))
            {
                ModelState.AddModelError("", "Collection image required.");
                return View(model);
            }
            var dtoModel = mapper.Map<CreateCollectionModel>(model);
            dtoModel.ImagePublicKey = imageId;
            dtoModel.OwnerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await collectionsManager.CreateAsync(dtoModel);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View(new CreateItemVM());
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var imageId = HttpContext.Session.GetString("fileId");
            if (string.IsNullOrEmpty(imageId))
            {
                ModelState.AddModelError("", "Collection image required.");
                return View(model);
            }
            var dtoModel = mapper.Map<CreateItemModel>(model);
            dtoModel.ImageUrl = imageId;
            dtoModel.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await itemsManager.CreateAsync(dtoModel);
            return View(model);
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
            var previousId = HttpContext.Session.GetString("fileId");
            if (!string.IsNullOrEmpty(previousId))
                await AbortUpload();
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

        [HttpPost]
        public async Task<IActionResult> GetCollectionFields(int id)
        {
            var collection = await collectionsCrudService.GetAsync(id);
            if (collection is null)
                return Json(null);
            var fieldsVM = mapper.Map<IEnumerable<OptionalFieldVM>>(collection.OptionalFields);
            return Json(fieldsVM);
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