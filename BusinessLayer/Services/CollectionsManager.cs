using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        private readonly IResourcesManager resourcesManager;

        private readonly ICollectionItemCrudService collectionItemCrudService;

        private readonly IMapper mapper;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IModelValidatorsStore validatorsStore, IModelAuthenticatorsStore authenticatorsStore, ICollectionItemCrudService collectionItemCrudService, IResourcesManager resourcesManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.collectionsCrudService = collectionsCrudService;
            this.validatorsStore = validatorsStore;
            this.authenticatorsStore = authenticatorsStore;
            this.resourcesManager = resourcesManager;
            this.collectionItemCrudService = collectionItemCrudService;
        }

        public async Task AttachItemToCollection(int itemId, int collectionId)
        {
            var allCollectionItems = await collectionItemCrudService.GetAllAsync();
            if (allCollectionItems.Any(ci => ci.CollectionId == collectionId && ci.ItemId == itemId))
                return;
            var collectionItem = new CollectionItemModel
            {
                CollectionId = collectionId,
                ItemId = itemId
            };
            await collectionItemCrudService.CreateAsync(collectionItem);
        }

        public async Task<CreateCollectionResult> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var authResult = await authenticatorsStore.CreateCollectionModelAuthenticator.AuthenticateAsync(createCollectionModel);
            if (!authResult.Succeed)
                return new CreateCollectionResult(authResult);
            var validResult = await validatorsStore.CreateCollectionModelValidator.ValidateAsync(createCollectionModel);
            if (!validResult.Succeed)
                return new CreateCollectionResult(validResult);
            var result = new CreateCollectionResult();
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            collectionModel.CreationDate = DateTime.Now;
            await collectionsCrudService.CreateAsync(collectionModel);
            result.CollectionId = collectionModel.Id;
            return result;
        }

        public async Task<DeleteCollectionResult> DeleteAsync(DeleteCollectionModel deleteCollectionModel)
        {
            var authResult = await authenticatorsStore.DeleteCollectionModelAuthenticator.AuthenticateAsync(deleteCollectionModel);
            if (!authResult.Succeed)
                return new DeleteCollectionResult(authResult);
            var validResult = await validatorsStore.DeleteCollectionModelValidator.ValidateAsync(deleteCollectionModel);
            if (!validResult.Succeed)
                return new DeleteCollectionResult(validResult);
            var result = new DeleteCollectionResult();
            var resource = (await collectionsCrudService.GetAsync(deleteCollectionModel.CollectionId)).Resource;
            await collectionsCrudService.DeleteAsync(deleteCollectionModel.CollectionId);
            await resourcesManager.DeleteAsync(resource);
            return result;
        }

        public async Task<UpdateCollectionResult> UpdateAsync(UpdateCollectionModel updateCollectionModel)
        {
            var authResult = await authenticatorsStore.UpdateCollectionModelAuthenticator.AuthenticateAsync(updateCollectionModel);
            if (!authResult.Succeed)
                return new UpdateCollectionResult(authResult);
            var validResult = await validatorsStore.UpdateCollectionModelValidator.ValidateAsync(updateCollectionModel);
            if (!validResult.Succeed)
                return new UpdateCollectionResult(validResult);
            var result = new UpdateCollectionResult();
            var collectionModel = mapper.Map<CollectionModel>(updateCollectionModel);
            await collectionsCrudService.UpdateAsync(collectionModel);
            return result;
        }
    }
}