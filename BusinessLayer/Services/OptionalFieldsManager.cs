using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
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

        private readonly IModelValidatorsStore validationUnitOfWork;

        public OptionalFieldsManager(IOptionalFieldsCrudService optionalFieldsCrudService, IFieldTypesCrudService fieldTypesCrudService, IModelValidatorsStore validationUnitOfWork)
        {
            this.optionalFieldsCrudService = optionalFieldsCrudService;
            this.fieldTypesCrudService = fieldTypesCrudService;
            this.validationUnitOfWork = validationUnitOfWork;
        }

        public async Task<CreateOptionalFieldResult> CreateDefaultAsync(CreateDefaultOptionalFieldModel createDefaultFieldModel)
        {
            var validationresult = await validationUnitOfWork.CreateDefaultOptionalFieldModelValidator.ValidateAsync(createDefaultFieldModel);
            var result = new CreateOptionalFieldResult(validationresult);
            if (!result.Succeed)
                return result;
            var model = new OptionalFieldModel
            {
                CollectionId = createDefaultFieldModel.CollectionId,
                Name = "Unnamed",
                TypeId = (await fieldTypesCrudService.GetAllAsync()).First().Id
            };
            await optionalFieldsCrudService.CreateAsync(model);
            result.OptionalFieldModel = model;
            return result;
        }

        public async Task<DeleteOptionalFieldResult> DeleteAsync(DeleteOptionalFieldModel deleteFieldModel)
        {
            var validationResult = await validationUnitOfWork.DeleteOptionalFieldModelValidator.ValidateAsync(deleteFieldModel);
            var result = new DeleteOptionalFieldResult(validationResult);
            if (!result.Succeed)
                return result;
            await optionalFieldsCrudService.DeleteAsync(deleteFieldModel.OptionalFieldId);
            return result;
        }
    }
}