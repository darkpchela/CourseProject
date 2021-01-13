using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IThemesCrudService themesCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        private readonly IMapper mapper;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IThemesCrudService themesCrudService,
            IUserCrudService userCrudService, IResourceCrudService resourceCrudService, IMapper mapper)
        {
            this.mapper = mapper;
            this.userCrudService = userCrudService;
            this.themesCrudService = themesCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        public async Task<CreateCollectionResult> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var result = await ValidateCreateCollectionModel(createCollectionModel);
            if (!result.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            await collectionsCrudService.CreateAsync(collectionModel);
            result.CollectionId = collectionModel.Id;
            return result;
        }

        public async Task<UpdateCollectionResult> UpdateAsync(UpdateCollectionModel updateCollectionModel)
        {
            var result = await ValidateUpdateCollectionModel(updateCollectionModel);
            if (!result.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(updateCollectionModel);
            await collectionsCrudService.UpdateAsync(collectionModel);
            return result;
        }

        private async Task<CreateCollectionResult> ValidateCreateCollectionModel(CreateCollectionModel createCollectionModel)
        {
            var result = new CreateCollectionResult();
            var owner = await userCrudService.GetAsync(createCollectionModel.OwnerId);
            if (owner is null)
                result.Errors.Add("User not found");
            var resource = await resourceCrudService.GetAsync(createCollectionModel.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
            var theme = await themesCrudService.GetAsync(createCollectionModel.ThemeId);
            if (theme is null)
                result.Errors.Add("Theme not found");
            if (!result.Succeed)
                return result;

            if (!createCollectionModel.IsAdminRequest && createCollectionModel.RequesterId != owner.Id)
                result.AddError("Access denied");
            return result;
        }

        private async Task<UpdateCollectionResult> ValidateUpdateCollectionModel(UpdateCollectionModel updateCollectionModel)
        {
            var result = new UpdateCollectionResult();
            var collection = await collectionsCrudService.GetAsync(updateCollectionModel.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(updateCollectionModel.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var resource = await resourceCrudService.GetAsync(updateCollectionModel.ResourceId);
            if (resource is null)
                result.AddError("Resource not foun");
            var theme = await themesCrudService.GetAsync(updateCollectionModel.ThemeId);
            if (theme is null)
                result.AddError("Theme not found");
            if (!result.Succeed)
                return result;

            if (collection.OwnerId != owner.Id)
                result.AddError("Owner matching failed");
            if (!updateCollectionModel.IsAdminRequest && owner.Id != updateCollectionModel.RequesterId)
                result.AddError("Access denied");
            return result;
        }
    }
}