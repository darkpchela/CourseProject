using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Linq;
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

        public async Task<CreateResourceResult> CreateAsync(CreateResourceModel createResourceModel)
        {
            var resultModel = new CreateResourceResult();
            var cloudinaryRes = await cloudinaryService.UploadAsync(createResourceModel.Name, createResourceModel.FileStream);
            if (cloudinaryRes.Error != null || cloudinaryRes.Uri is null)
            {
                resultModel.AddError("Cloudinary service error");
                return resultModel;
            }
            var resourceModel = mapper.Map<ResourceModel>(cloudinaryRes);
            await resourceCrudService.CreateAsync(resourceModel);
            mapper.Map(resourceModel, resultModel);
            return resultModel;
        }

        public async Task DeleteAsync(ResourceModel resourceModel)
        {
            if (resourceModel is null)
                return;
            await cloudinaryService.DeleteAsync(resourceModel.PublicId);
            await resourceCrudService.DeleteAsync(resourceModel.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await resourceCrudService.GetAsync(id);
            if (resource is null)
                return;
            await cloudinaryService.DeleteAsync(resource.PublicId);
            await resourceCrudService.DeleteAsync(id);
        }

        public async Task RemoveGarbageAsync()
        {
            var allResources = await resourceCrudService.GetAllAsync();
            var garbage = allResources.Where(r => r.Collections.Count() == 0 && r.Items.Count() == 0);
            foreach (var resource in garbage)
            {
                await DeleteAsync(resource);
            }
        }
    }
}