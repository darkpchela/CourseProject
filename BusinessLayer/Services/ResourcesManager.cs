using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ResourcesManager : IResourcesManager
    {
        private readonly IResourceCrudService resourceCrudService;

        private readonly ICloudinaryService cloudinaryService;

        private readonly IMapper mapper;

        public ResourcesManager(IResourceCrudService resourceCrudService, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.resourceCrudService = resourceCrudService;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public async Task<CreateResourceResultModel> CreateAsync(CreateResourceModel createResourceModel)
        {
            var resultModel = new CreateResourceResultModel();
            var cloudinaryRes = await cloudinaryService.UploadAsync(createResourceModel.Name, createResourceModel.FileStream);
            if (cloudinaryRes.Error != null || cloudinaryRes.Uri is null)
            {
                resultModel.AddError("Cloudinary service error");
                return resultModel;
            }

            return resultModel;

            //var resultModel = new CreateResourceResultModel
            //{
            //    Succeed = cloudinaryRes.Error is null ? true : false,
            //    Error = cloudinaryRes.Error?.Message ?? "",
            //    Uri = cloudinaryRes.Uri?.AbsoluteUri ?? "",
            //    ObjectId = cloudinaryRes.PublicId
            //};
        }
    }
}
