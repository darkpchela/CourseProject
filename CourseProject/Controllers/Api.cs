using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class Api : Controller
    {
        private readonly IMapper mapper;

        private readonly IResourcesManager resourcesManager;

        public Api(IMapper mapper, IResourcesManager resourcesManager)
        {
            this.mapper = mapper;
            this.resourcesManager = resourcesManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var resourcModel = mapper.Map<CreateResourceModel>(file);
            var res = await resourcesManager.CreateAsync(resourcModel);
            if (res.Succeed)
                HttpContext.Session.SetInt32("fileId", res.Id);
            return Json(res);
        }

        [HttpPost]
        public async Task AbortUpload()
        {
            var fileId = HttpContext.Session.GetInt32("fileId");
            if (fileId.HasValue)
                await resourcesManager.DeleteAsync(fileId.Value);
        }
    }
}