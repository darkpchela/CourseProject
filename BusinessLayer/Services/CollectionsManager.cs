using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        private readonly IMapper mapper;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IModelValidatorsStore validatorsStore, IModelAuthenticatorsStore authenticatorsStore, IMapper mapper)
        {
            this.mapper = mapper;
            this.collectionsCrudService = collectionsCrudService;
            this.validatorsStore = validatorsStore;
            this.authenticatorsStore = authenticatorsStore;
        }

        public async Task<CreateCollectionResult> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var validResult = await validatorsStore.CreateCollectionModelValidator.ValidateAsync(createCollectionModel);
            if (!validResult.Succeed)
                return new CreateCollectionResult(validResult);
            var authResult = await authenticatorsStore.CreateCollectionModelAuthenticator.AuthenticateAsync(createCollectionModel);
            if (!authResult.Succeed)
                return new CreateCollectionResult(authResult);
            var result = new CreateCollectionResult();
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            await collectionsCrudService.CreateAsync(collectionModel);
            result.CollectionId = collectionModel.Id;
            return result;
        }

        public async Task<DeleteCollectionResult> DeleteAsync(DeleteCollectionModel deleteCollectionModel)
        {
            var validResult = await validatorsStore.DeleteCollectionModelValidator.ValidateAsync(deleteCollectionModel);
            if (!validResult.Succeed)
                return new DeleteCollectionResult(validResult);
            var authResult = await authenticatorsStore.DeleteCollectionModelAuthenticator.AuthenticateAsync(deleteCollectionModel);
            if (!authResult.Succeed)
                return new DeleteCollectionResult(authResult);
            var result = new DeleteCollectionResult();
            await collectionsCrudService.DeleteAsync(deleteCollectionModel.CollectionId);
            return result;
        }

        public async Task<UpdateCollectionResult> UpdateAsync(UpdateCollectionModel updateCollectionModel)
        {
            var validResult = await validatorsStore.UpdateCollectionModelValidator.ValidateAsync(updateCollectionModel);
            if (!validResult.Succeed)
                return new UpdateCollectionResult(validResult);
            var authResult = await authenticatorsStore.UpdateCollectionModelAuthenticator.AuthenticateAsync(updateCollectionModel);
            if (!authResult.Succeed)
                return new UpdateCollectionResult(authResult);
            var result = new UpdateCollectionResult();
            var collectionModel = mapper.Map<CollectionModel>(updateCollectionModel);
            await collectionsCrudService.UpdateAsync(collectionModel);
            return result;
        }
    }
}