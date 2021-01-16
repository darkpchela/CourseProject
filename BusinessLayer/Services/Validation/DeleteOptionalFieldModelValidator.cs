using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class DeleteOptionalFieldModelValidator : DefaultValidator<DeleteOptionalFieldModel>, IDeleteOptionalFieldModelValidator
    {
        private readonly IOptionalFieldsCrudService optionalFieldsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        public DeleteOptionalFieldModelValidator(IOptionalFieldsCrudService optionalFieldsCrudService, ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.userCrudService = userCrudService;
            this.optionalFieldsCrudService = optionalFieldsCrudService;
        }

        protected async override Task BaseValidation(DeleteOptionalFieldModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.AddError("User not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                ValidationResult.AddError("Collection not found");
            var field = await optionalFieldsCrudService.GetAsync(model.OptionalFieldId);
            if (field is null)
                ValidationResult.AddError("Optional field not found");
        }

        protected async override Task OptionalValidation(DeleteOptionalFieldModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            var field = await optionalFieldsCrudService.GetAsync(model.OptionalFieldId);
            if (!collection.OptionalFields.Any(f => f.Id == field.Id))
                ValidationResult.AddError("Optional field not found");
            if (model.OwnerId != collection.OwnerId)
                ValidationResult.AddError("Owner matching error");
        }
    }
}