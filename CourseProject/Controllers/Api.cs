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

        public Api(IMapper mapper, IResourcesManager resourcesManager, ICollectionsCrudService collectionsCrudService, IOptionalFieldsManager optionalFieldsManager)
        {
            this.mapper = mapper;
            this.resourcesManager = resourcesManager;
            this.collectionsCrudService = collectionsCrudService;
            this.optionalFieldsManager = optionalFieldsManager;
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
        public async Task<IActionResult> DeleteCollection()
        {
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem()
        {
            return Json(true);
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
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var dtoModel = mapper.Map<CreateDefaultOptionalFieldModel>(model);
            var result = await optionalFieldsManager.CreateDefaultAsync(dtoModel);
            var resultVM = mapper.Map<CreateOptionalFieldResultVM>(result);
            return Json(resultVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteField(DeleteOptionalFieldVM model)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var dtoModel = mapper.Map<DeleteOptionalFieldModel>(model);
            var result = await optionalFieldsManager.DeleteAsync(dtoModel);
            return Json(result);
        }
    }
}