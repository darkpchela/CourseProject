using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using CourseProject.ViewModels.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Api : Controller
    {
        private readonly IMapper mapper;

        private readonly ITagsCrudService tagsCrudService;

        private readonly IResourcesManager resourcesManager;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IOptionalFieldsManager optionalFieldsManager;

        private readonly IItemsCrudService itemsCrudService;

        private readonly IItemsManager itemsManager;

        private readonly ICollectionsManager collectionsManager;

        private readonly ILikesManager likesManager;

        private readonly ICommentsManager commentsManager;

        public Api(IMapper mapper, ITagsCrudService tagsCrudService, IResourcesManager resourcesManager, ICollectionsCrudService collectionsCrudService, IOptionalFieldsManager optionalFieldsManager, IItemsManager itemsManager,
            ICollectionsManager collectionsManager, IItemsCrudService itemsCrudService, ILikesManager likesManager, ICommentsManager commentsManager)
        {
            this.mapper = mapper;
            this.resourcesManager = resourcesManager;
            this.collectionsCrudService = collectionsCrudService;
            this.optionalFieldsManager = optionalFieldsManager;
            this.itemsManager = itemsManager;
            this.collectionsManager = collectionsManager;
            this.tagsCrudService = tagsCrudService;
            this.itemsCrudService = itemsCrudService;
            this.likesManager = likesManager;
            this.commentsManager = commentsManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var resourcModel = mapper.Map<CreateResourceModel>(file);
            var result = await resourcesManager.CreateAsync(resourcModel);
            if (result.Succeed)
                HttpContext.Session.SetInt32("fileId", result.Id);
            return Json(result);
        }

        [HttpPost]
        public async Task AbortUpload()
        {
            var fileId = HttpContext.Session.GetInt32("fileId");
            if (fileId.HasValue)
                await resourcesManager.DeleteAsync(fileId.Value);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCollection(DeleteCollectionVM model)
        {
            var dtoModel = mapper.Map<DeleteCollectionModel>(model);
            var result = await collectionsManager.DeleteAsync(dtoModel);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(DeleteItemVM model)
        {
            var dtoModel = mapper.Map<DeleteItemModel>(model);
            var result = await itemsManager.DeleteAsync(dtoModel);
            return Json(result);
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

        [HttpPost]
        public async Task<IActionResult> CreateField(CreateDefaultOptionalFieldVM model)
        {
            var dtoModel = mapper.Map<CreateOptionalFieldModel>(model);
            var result = await optionalFieldsManager.CreateAsync(dtoModel);
            var resultVM = mapper.Map<CreateOptionalFieldResultVM>(result);
            return Json(resultVM);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteField(DeleteOptionalFieldVM model)
        {
            var dtoModel = mapper.Map<DeleteOptionalFieldModel>(model);
            var result = await optionalFieldsManager.DeleteAsync(dtoModel);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetTags()
        {
            var allTags = await tagsCrudService.GetAllAsync();
            string[] topTags = allTags.OrderByDescending(t => t.ItemTags.Count()).Take(20).Select(t => t.Value).ToArray();
            return Json(topTags);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemTags(int id)
        {
            var tags = (await itemsCrudService.GetAsync(id)).ItemTags.Select(it => it.Tag).ToList();
            var tagVM = mapper.Map<IEnumerable<TagJsonVM>>(tags);
            return Json(tagVM);
        }

        [HttpPost]
        public async Task<IActionResult> LikeItem(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int requesterId);
            var model = new LikeItemModel
            {
                ItemId = id,
                UserId = requesterId
            };
            var result = await likesManager.LikeItemAsync(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> CommentItem(int itemId, string value)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int requesterId);
            var model = new CommentItemModel
            {
                ItemId = itemId,
                UserId = requesterId,
                Value = value
            };
            var result = await commentsManager.CommentItemAsync(model);
            return Json(result);
        }
    }
}