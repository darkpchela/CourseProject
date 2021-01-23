using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
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
        private readonly ICollectionsManager collectionsManager;

        private readonly IItemsManager itemsManager;

        private readonly IItemsCrudService itemsCrudService;

        private readonly IMapper mapper;

        private readonly ICollectionsCrudService collectionsCrudService;

        public Store(ICollectionsManager collectionsManager, IItemsManager itemsManager, IItemsCrudService itemsCrudService, ICollectionsCrudService collectionsCrudService, IMapper mapper)
        {
            this.collectionsManager = collectionsManager;
            this.itemsManager = itemsManager;
            this.itemsCrudService = itemsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.mapper = mapper;
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