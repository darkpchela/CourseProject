using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class OptionalFieldsManager : IOptionalFieldsManager
    {
        private readonly IOptionalFieldsCrudService optionalFieldsCrudService;

        private readonly IFieldTypesCrudService fieldTypesCrudService;

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        public OptionalFieldsManager(IOptionalFieldsCrudService optionalFieldsCrudService, IFieldTypesCrudService fieldTypesCrudService, IModelValidatorsStore validatorsStore, IModelAuthenticatorsStore authenticatorsStore)
        {
            this.optionalFieldsCrudService = optionalFieldsCrudService;
            this.fieldTypesCrudService = fieldTypesCrudService;
            this.validatorsStore = validatorsStore;
            this.authenticatorsStore = authenticatorsStore;
        }

        public async Task<CreateOptionalFieldResult> CreateDefaultAsync(CreateDefaultOptionalFieldModel createDefaultFieldModel)
        {
            var validResult = await validatorsStore.CreateDefaultOptionalFieldModelValidator.ValidateAsync(createDefaultFieldModel);
            if (!validResult.Succeed)
                return new CreateOptionalFieldResult(validResult);
            var authResult = await authenticatorsStore.CreateOptionalFieldModelAuthenticator.AuthenticateAsync(createDefaultFieldModel);
            if (!authResult.Succeed)
                return new CreateOptionalFieldResult(authResult);
            var result = new CreateOptionalFieldResult();
            var model = await GetDefaultFieldModel(createDefaultFieldModel.CollectionId);
            await optionalFieldsCrudService.CreateAsync(model);
            result.OptionalFieldModel = model;
            return result;
        }

        public async Task<DeleteOptionalFieldResult> DeleteAsync(DeleteOptionalFieldModel deleteFieldModel)
        {
            var validResult = await validatorsStore.DeleteOptionalFieldModelValidator.ValidateAsync(deleteFieldModel);
            if (!validResult.Succeed)
                return new DeleteOptionalFieldResult(validResult);
            var authResult = await authenticatorsStore.DeleteOptionalFieldModelAuthenticator.AuthenticateAsync(deleteFieldModel);
            if (!authResult.Succeed)
                return new DeleteOptionalFieldResult(authResult);
            var result = new DeleteOptionalFieldResult();
            await optionalFieldsCrudService.DeleteAsync(deleteFieldModel.OptionalFieldId);
            return result;
        }

        private async Task<OptionalFieldModel> GetDefaultFieldModel(int collectionId)
        {
            return new OptionalFieldModel
            {
                CollectionId = collectionId,
                Name = "Unnamed",
                TypeId = (await fieldTypesCrudService.GetAllAsync()).First().Id
            };
        }
    }
}