using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using CourseProject.ViewModels;
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
        private readonly IResourcesManager resourcesManager;

        private readonly ICollectionsManager collectionsManager;

        private readonly IUserCrudService userCrudService;

        private readonly IItemsManager itemsManager;

        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IMapper mapper;

        private CreateCollectionVM createCollectionTestVM = new CreateCollectionVM()
        {
            Name = "Test",
            Description = "It's a test collection"
        };

        public Store(IResourcesManager resourcesManager, ICollectionsManager collectionsManager, IItemsManager itemsManager, IUserCrudService userCrudService,
            IItemsCrudService itemsCrudService, ICollectionsCrudService collectionsCrudService, IMapper mapper)
        {
            this.resourcesManager = resourcesManager;
            this.collectionsManager = collectionsManager;
            this.itemsManager = itemsManager;
            this.collectionsCrudService = collectionsCrudService;
            this.itemsCrudService = itemsCrudService;
            this.userCrudService = userCrudService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Profile(int? id)
        {
            if (id is null)
                id = GetCurrentUserId();
            var user = await userCrudService.GetAsync(id.Value);
            var model = mapper.Map<UserVM>(user);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollection()
        {
            var ownerId = GetCurrentUserId();
            if (ownerId is null)
                return RedirectToAction(nameof(Account.SignIn), nameof(Account));
            var model = createCollectionTestVM;
            model.OwnerId = ownerId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection(CreateCollectionVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var dtoModel = mapper.Map<CreateCollectionModel>(model);
            var result = await collectionsManager.CreateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCollection(int id)
        {
            var collection = await collectionsCrudService.GetAsync(id);
            if (collection is null)
                return RedirectToAction(nameof(Profile), nameof(Store));
            var model = mapper.Map<EditCollectionVM>(collection);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCollection(EditCollectionVM editCollectionVM)
        {
            if (!ModelState.IsValid)
                return View(editCollectionVM);
            var model = mapper.Map<UpdateCollectionModel>(editCollectionVM);
            var result = await collectionsManager.UpdateAsync(model);
            if(!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }
            return RedirectToAction(nameof(Collection), nameof(Store), new { id = model.Id });
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            var ownerId = GetCurrentUserId();
            if (ownerId is null)
                return RedirectToAction(nameof(Account.SignIn), nameof(Account));
            var model = new CreateItemVM
            {
                OwnerId = ownerId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var dtoModel = mapper.Map<CreateItemModel>(model);
            var result = await itemsManager.CreateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Collection(int id)
        {
            var collection = await collectionsCrudService.GetAsync(id);
            if (collection is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            var collectionVM = mapper.Map<CollectionVM>(collection);
            return View(collectionVM);
        }

        public async Task<IActionResult> Item(int id)
        {
            var item = await itemsCrudService.GetAsync(id);
            if (item is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            var itemVM = mapper.Map<ItemVM>(item);
            return View(itemVM);
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

        private int? GetCurrentUserId()
        {
            var stringId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (stringId is null || !int.TryParse(stringId, out int id))
                return null;
            else
                return id;
        }
    }
}