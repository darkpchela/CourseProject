﻿using AutoMapper;
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
            var model = new CreateCollectionModel();
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
            return RedirectToAction(nameof(Collection), nameof(Store), new { id = result.CollectionId });
        }

        [HttpGet]
        public async Task<IActionResult> EditCollection(int id)
        {
            var collection = await collectionsCrudService.GetAsync(id);
            if (collection is null)
                return RedirectToAction(nameof(Profile), nameof(Store));
            if (!User.IsInRole("Admin") && collection.OwnerId != GetCurrentUserId())
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
            return RedirectToAction(nameof(Collection), nameof(Store), new { id = dtoModel.CollectionId });
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            var model = new CreateItemVM();
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
            var result = await itemsManager.CreateAsync(dtoModel);
            if (!result.Succeed)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e));
                return View(createItemVM);
            }
            return RedirectToAction(nameof(Item), nameof(Store), new { id = result.CreatedItemId });
        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            var item = await itemsCrudService.GetAsync(id);
            if (item is null)
                return RedirectToAction(nameof(Profile), nameof(Store));
            if (!User.IsInRole("Admin") && item.OwnerId != GetCurrentUserId())
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
            return RedirectToAction(nameof(Item), nameof(Store), new { id = dtoModel.ItemId });
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

        public async Task<IActionResult> Collections()
        {
            return View();
        }

        public async Task<IActionResult> Item(int id)
        {
            var item = await itemsCrudService.GetAsync(id);
            if (item is null)
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            RememberUserId(item.OwnerId);
            var itemVM = mapper.Map<ItemVM>(item);
            itemVM.Liked = item.ItemLikes.Any(il => il.UserId == GetCurrentUserId());
            return View(itemVM);
        }

        public async Task<IActionResult> Items()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchItems(string text)
        {
            var items = await itemsManager.SearchAsync(text);
            var itemsVM = mapper.Map<IEnumerable<ItemVM>>(items);
            return PartialView("_SearchItems", itemsVM);
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