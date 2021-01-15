using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class CreateDefaultOptionalFieldModelValidator : DefaultValidator<CreateDefaultOptionalFieldModel>, ICreateDefaultOptionalFieldModelValidator
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

        protected async override Task BaseValidation(CreateDefaultOptionalFieldModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var type = (await fieldTypesCrudService.GetAllAsync()).FirstOrDefault();
            if (type is null)
                result.AddError("Available field types not found ");
        }

        protected async override Task OptionalValidation(CreateDefaultOptionalFieldModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection.OwnerId != model.OwnerId)
                result.AddError("Owner matching error");
        }
    }
}