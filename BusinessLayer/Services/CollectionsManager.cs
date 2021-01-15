using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IModelValidatorsStore validationUnitOfWork;

        private readonly IMapper mapper;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IModelValidatorsStore validationUnitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.collectionsCrudService = collectionsCrudService;
            this.validationUnitOfWork = validationUnitOfWork;
        }

        public async Task<CreateCollectionResult> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var validationResult = await validationUnitOfWork.CreateCollectionModelValidator.ValidateAsync(createCollectionModel);
            var result = new CreateCollectionResult(validationResult);
            if (!result.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            await collectionsCrudService.CreateAsync(collectionModel);
            result.CollectionId = collectionModel.Id;
            return result;
        }

        public Task<DeleteCollectionResult> DeleteAsync(DeleteCollectionModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UpdateCollectionResult> UpdateAsync(UpdateCollectionModel updateCollectionModel)
        {
            var validationRes = await validationUnitOfWork.UpdateCollectionModelValidator.ValidateAsync(updateCollectionModel);
            var result = new UpdateCollectionResult(validationRes);
            if (!validationRes.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(updateCollectionModel);
            await collectionsCrudService.UpdateAsync(collectionModel);
            return result;
        }
    }
}