using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Profile : Controller
    {
        private readonly IMapper mapper;

        private readonly ISessionHelper sessionHelper;

        private readonly IUserCrudService userCrudService;

        private readonly ICollectionsManager collectionsManager;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IItemsManager itemsManager;

        private readonly IItemsCrudService itemsCrudService;

        public Profile(IMapper mapper, ISessionHelper sessionHelper, IUserCrudService userCrudService, ICollectionsManager collectionsManager, ICollectionsCrudService collectionsCrudService, IItemsManager itemsManager, IItemsCrudService itemsCrudService)
        {
            this.mapper = mapper;
            this.userCrudService = userCrudService;
            this.collectionsManager = collectionsManager;
            this.collectionsCrudService = collectionsCrudService;
            this.itemsManager = itemsManager;
            this.itemsCrudService = itemsCrudService;
            this.sessionHelper = sessionHelper;
        }

        public async Task<IActionResult> Info(int? id)
        {
            if (id is null)
            {
                if (User.IsInRole("admin"))
                    return RedirectToAction(nameof(Admin.ContinueAs), nameof(Admin));
                else
                    id = sessionHelper.GetCurrentUserId();
            }
            var user = await userCrudService.GetAsync(id.Value);
            if (user is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            sessionHelper.RememberUserId(id.Value);
            var model = mapper.Map<UserVM>(user);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollection()
        {
            var ownerId = User.IsInRole("admin") ? sessionHelper.GetRemeberedUserId() : sessionHelper.GetCurrentUserId();
            if (ownerId == 0)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            var model = new CreateCollectionVM();
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
            return RedirectToAction(nameof(Store.Collection), nameof(Store), new { id = result.CollectionId });
        }

        [HttpGet]
        public async Task<IActionResult> EditCollection(int id)
        {
            var collection = await collectionsCrudService.GetAsync(id);
            if (collection is null)
                return RedirectToAction(nameof(Profile), nameof(Store));
            if (!User.IsInRole("admin") && collection.OwnerId != sessionHelper.GetCurrentUserId())
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            var editVM = mapper.Map<EditCollectionVM>(collection);
            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditCollection(EditCollectionVM editCollectionVM)
        {
            if (!ModelState.IsValid)
                return View(editCollectionVM);
            var dtoModel = mapper.Map<UpdateCollectionModel>(editCollectionVM);
            var result = await collectionsManager.UpdateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(editCollectionVM);
            }
            return RedirectToAction(nameof(Store.Collection), nameof(Store), new { id = dtoModel.CollectionId });
        }

        [HttpGet]
        public IActionResult CreateItem(int? collectionId)
        {
            var model = new CreateItemVM();
            var ownerId = User.IsInRole("admin") ? sessionHelper.GetRemeberedUserId() : sessionHelper.GetCurrentUserId();
            if (ownerId == 0)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            model.OwnerId = ownerId;
            if (collectionId.HasValue)
                model.CollectionId = collectionId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemVM createItemVM)
        {
            if (!ModelState.IsValid)
                return View(createItemVM);
            var dtoModel = mapper.Map<CreateItemModel>(createItemVM);
            var result = await itemsManager.CreateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(createItemVM);
            }
            return RedirectToAction(nameof(Store.Item), nameof(Store), new { id = result.CreatedItemId });
        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            var item = await itemsCrudService.GetAsync(id);
            if (item is null)
                return RedirectToAction(nameof(Profile.Info), nameof(Profile));
            if (!User.IsInRole("admin") && item.OwnerId != sessionHelper.GetCurrentUserId())
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            var editVM = mapper.Map<EditItemVM>(item);
            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(EditItemVM editItemVM)
        {
            if (!ModelState.IsValid)
                return View(editItemVM);
            var dtoModel = mapper.Map<UpdateItemModel>(editItemVM);
            var result = await itemsManager.UpdateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(editItemVM);
            }
            return RedirectToAction(nameof(Store.Item), nameof(Store), new { id = dtoModel.ItemId });
        }
    }
}