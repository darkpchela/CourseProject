using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
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

        public async Task<CreateCollectionResultModel> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var result = await ValidateCreateCollectionModel(createCollectionModel);
            if (!result.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            await collectionsCrudService.CreateAsync(collectionModel);
            result.CollectionId = collectionModel.Id;
            return result;
        }

        public async Task<UpdateCollectionResultModel> UpdateAsync(UpdateCollectionModel updateCollectionModel)
        {
            var result = await ValidateUpdateCollectionModel(updateCollectionModel);
            if (!result.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(updateCollectionModel);
            await collectionsCrudService.UpdateAsync(collectionModel);
            return result;
        }

        private async Task<CreateCollectionResultModel> ValidateCreateCollectionModel(CreateCollectionModel createCollectionModel)
        {
            var result = new CreateCollectionResultModel();
            var theme = await themesCrudService.GetAsync(createCollectionModel.ThemeId);
            if (theme is null)
                result.Errors.Add("Theme not exists");
            var user = await userCrudService.GetAsync(createCollectionModel.OwnerId);
            if (user is null)
                result.Errors.Add("User not exists");
            var resource = await resourceCrudService.GetAsync(createCollectionModel.ResourceId);
            if (resource is null)
                result.AddError("Resource not exists");
            return result;
        }

        private async Task<UpdateCollectionResultModel> ValidateUpdateCollectionModel(UpdateCollectionModel updateCollectionModel)
        {
            var result = new UpdateCollectionResultModel();
            //var collection = await collectionsCrudService.GetAsync(updateCollectionModel.Id);
            //if (collection is null)
            //    result.AddError("Collection not exists");
            var user = await userCrudService.GetAsync(updateCollectionModel.OwnerId);
            if (user is null)
                result.AddError("User not exists");
            var resource = await resourceCrudService.GetAsync(updateCollectionModel.ResourceId);
            if (resource is null)
                result.AddError("Resource not exists");
            var theme = await themesCrudService.GetAsync(updateCollectionModel.ThemeId);
            if (theme is null)
                result.AddError("Theme not exists");
            return result;
        }
    }
}