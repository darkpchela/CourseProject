using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class CreateDefaultOptionalFieldModelValidator : DefaultValidator<CreateOptionalFieldModel>, ICreateDefaultOptionalFieldModelValidator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IFieldTypesCrudService fieldTypesCrudService;

        private readonly IUserCrudService userCrudService;

        public CreateDefaultOptionalFieldModelValidator(ICollectionsCrudService collectionsCrudService, IFieldTypesCrudService fieldTypesCrudService, IUserCrudService userCrudService)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.fieldTypesCrudService = fieldTypesCrudService;
            this.userCrudService = userCrudService;
        }

        protected async override Task BaseValidation(CreateOptionalFieldModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.AddError("User not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                ValidationResult.AddError("Collection not found");
            var type = (await fieldTypesCrudService.GetAllAsync()).FirstOrDefault();
            if (type is null)
                ValidationResult.AddError("Available field types not found ");
        }

        protected async override Task OptionalValidation(CreateOptionalFieldModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection.OwnerId != model.OwnerId)
                ValidationResult.AddError("Owner matching error");
        }
    }
}