using AutoMapper;
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

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        private readonly IMapper mapper;

        public OptionalFieldsManager(IOptionalFieldsCrudService optionalFieldsCrudService, IModelValidatorsStore validatorsStore, IModelAuthenticatorsStore authenticatorsStore, IMapper mapper)
        {
            this.optionalFieldsCrudService = optionalFieldsCrudService;
            this.validatorsStore = validatorsStore;
            this.authenticatorsStore = authenticatorsStore;
            this.mapper = mapper;
        }

        public async Task<CreateOptionalFieldResult> CreateAsync(CreateOptionalFieldModel createFieldModel)
        {
            var validResult = await validatorsStore.CreateDefaultOptionalFieldModelValidator.ValidateAsync(createFieldModel);
            if (!validResult.Succeed)
                return new CreateOptionalFieldResult(validResult);
            var authResult = await authenticatorsStore.CreateOptionalFieldModelAuthenticator.AuthenticateAsync(createFieldModel);
            if (!authResult.Succeed)
                return new CreateOptionalFieldResult(authResult);
            var result = new CreateOptionalFieldResult();
            var model = mapper.Map<OptionalFieldModel>(createFieldModel);
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
    }
}