using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (user is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            var model = mapper.Map<UserVM>(user);
            RememberUserId(id.Value);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollection()
        {
            var ownerId = User.IsInRole("Admin") ? GetRememberedUserId() : GetCurrentUserId();
            if (ownerId == 0)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
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
            dtoModel.RequesterId = GetCurrentUserId();
            dtoModel.IsAdminRequest = User.IsInRole("Admin");
            var result = await collectionsManager.CreateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }
            return RedirectToAction(nameof(Collection), nameof(Store), new { id = result.CollectionId });
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
            model.RequesterId = GetCurrentUserId();
            model.IsAdminRequest = User.IsInRole("Admin");
            var result = await collectionsManager.UpdateAsync(model);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }
            return RedirectToAction(nameof(Collection), nameof(Store), new { id = model.Id });
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            var model = new CreateItemModel();
            var ownerId = User.IsInRole("Admin") ? GetRememberedUserId() : GetCurrentUserId();
            if (ownerId == 0)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            model.OwnerId = ownerId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemVM createItemVM)
        {
            if (!ModelState.IsValid)
                return View(createItemVM);
            var dtoModel = mapper.Map<CreateItemModel>(createItemVM);
            dtoModel.RequesterId = GetCurrentUserId();
            dtoModel.IsAdminRequest = User.IsInRole("Admin");
            var result = await itemsManager.CreateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(createItemVM);
            }
            return RedirectToAction(nameof(Item), nameof(Store), new { id = result.ItemId });
        }

        public async Task<IActionResult> Collection(int id)
        {
            var collection = await collectionsCrudService.GetAsync(id);
            if (collection is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            RememberUserId(collection.OwnerId);
            var collectionVM = mapper.Map<CollectionVM>(collection);
            return View(collectionVM);
        }

        public async Task<IActionResult> Item(int id)
        {
            var item = await itemsCrudService.GetAsync(id);
            if (item is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            RememberUserId(item.OwnerId);
            var itemVM = mapper.Map<ItemVM>(item);
            return View(itemVM);
        }


        private int GetCurrentUserId()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            return userId;
        }

        private void RememberUserId(int userId)
        {
            if (User.IsInRole("Admin"))
                HttpContext.Session.SetInt32("userId", userId);
        }

        private int GetRememberedUserId()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (!id.HasValue)
                return 0;
            return id.Value;
        }
    }
}