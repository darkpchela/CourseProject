using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using CourseProject.ViewModels.ApiModels;
using Microsoft.AspNetCore.Http;
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

        private readonly IResourcesManager resourcesManager;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IOptionalFieldsManager optionalFieldsManager;

        private readonly IItemsManager itemsManager;

        private readonly ICollectionsManager collectionsManager;

        public Api(IMapper mapper, IResourcesManager resourcesManager, ICollectionsCrudService collectionsCrudService, IOptionalFieldsManager optionalFieldsManager, IItemsManager itemsManager,
            ICollectionsManager collectionsManager)
        {
            this.mapper = mapper;
            this.resourcesManager = resourcesManager;
            this.collectionsCrudService = collectionsCrudService;
            this.optionalFieldsManager = optionalFieldsManager;
            this.itemsManager = itemsManager;
            this.collectionsManager = collectionsManager;
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
            string[] testTags = { "tag1", "tag2" };
            return Json(testTags);
        }
    }
}